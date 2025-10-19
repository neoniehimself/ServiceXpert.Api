using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServiceXpert.Application.DataObjects.Security;
using ServiceXpert.Application.Enums;
using ServiceXpert.Application.Models;
using ServiceXpert.Application.Models.Auth;
using ServiceXpert.Application.Services.Contracts.Security;
using ServiceXpert.Domain.Entities.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceXpert.Infrastructure.Services.Security;
internal class SecurityUserService : ISecurityUserService
{
    private readonly UserManager<SecurityUser> userManager;
    private readonly IConfiguration configuration;
    private readonly SxpConfiguration sxpConfiguration;
    private readonly ISecurityProfileService aspNetUserProfileService;
    private readonly RoleManager<SecurityRole> roleManager;

    public SecurityUserService(
        UserManager<SecurityUser> userManager,
        IConfiguration configuration,
        IOptions<SxpConfiguration> options,
        ISecurityProfileService aspNetUserProfileService,
        RoleManager<SecurityRole> roleManager
        )
    {
        this.userManager = userManager;
        this.configuration = configuration;
        this.sxpConfiguration = options.Value;
        this.aspNetUserProfileService = aspNetUserProfileService;
        this.roleManager = roleManager;
    }

    public async Task<ServiceResult<string>> LoginAsync(LoginUser loginUser)
    {
        var securityUser = await this.userManager.FindByNameAsync(loginUser.UserName);

        if (securityUser != null && await this.userManager.CheckPasswordAsync(securityUser, loginUser.Password))
        {
            var securityUserRoles = await this.userManager.GetRolesAsync(securityUser);

            if (!securityUserRoles.Any())
            {
                return ServiceResult<string>.Fail(ServiceResultStatus.Unauthorized, ["No roles assigned!"]);
            }

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, securityUser.Id.ToString()), // Equivalent of  ClaimTypes.NameIdentifier
                new(JwtRegisteredClaimNames.UniqueName, securityUser.UserName!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            claims.AddRange(securityUserRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.sxpConfiguration.JwtSecretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken
            (
                issuer: this.configuration["Jwt:Issuer"],
                audience: this.configuration["Jwt:Audience"],
                signingCredentials: signingCredentials,
                claims: claims,
                expires: DateTimeOffset.UtcNow.AddMinutes(Convert.ToInt16(this.configuration["Jwt:ExpiresInMinutes"])).UtcDateTime
            );

            return ServiceResult<string>.Ok(new JwtSecurityTokenHandler().WriteToken(securityToken));
        }

        return ServiceResult<string>.Fail(ServiceResultStatus.Unauthorized, ["Invalid username or password!"]);
    }

    public async Task<ServiceResult<Guid>> RegisterAsync(RegisterUser registerUser)
    {
        var result = await this.userManager.CreateAsync(new SecurityUser()
        {
            UserName = registerUser.UserName,
            Email = registerUser.Email,
            IsActive = true
        }, registerUser.Password);

        if (!result.Succeeded)
        {
            return ServiceResult<Guid>.Fail(ServiceResultStatus.ValidationError, result.Errors.Select(e => e.Description));
        }

        var securityUser = await this.userManager.FindByNameAsync(registerUser.UserName);

        var config = new TypeAdapterConfig();
        config.ForType<RegisterUser, CreateSecurityProfileDataObject>()
              .MapToConstructor(true)
              .ConstructUsing(src => new CreateSecurityProfileDataObject(securityUser!.Id));

        var securityProfile = registerUser.Adapt<CreateSecurityProfileDataObject>(config);
        await this.aspNetUserProfileService.CreateAsync(securityProfile);

        return ServiceResult<Guid>.Ok(securityUser!.Id);
    }

    public async Task<ServiceResult> AssignRoleAsync(UserRole userRole)
    {
        var securityUser = await this.userManager.FindByNameAsync(userRole.UserName);

        if (securityUser == null)
        {
            return ServiceResult.Fail(ServiceResultStatus.NotFound, ["User not found!"]);
        }

        if (!await this.roleManager.RoleExistsAsync(userRole.RoleName))
        {
            return ServiceResult.Fail(ServiceResultStatus.NotFound, ["Role not found!"]);
        }

        if (await this.userManager.IsInRoleAsync(securityUser, userRole.RoleName))
        {
            var errorMsg = $"User {userRole.UserName} is already assigned with role: {userRole.RoleName}!";
            return ServiceResult.Fail(ServiceResultStatus.ValidationError, [errorMsg]);
        }

        var result = await this.userManager.AddToRoleAsync(securityUser, userRole.RoleName);
        return result.Succeeded
            ? ServiceResult.Ok()
            : ServiceResult.Fail(ServiceResultStatus.InternalError, result.Errors.Select(e => e.Description));
    }
}
