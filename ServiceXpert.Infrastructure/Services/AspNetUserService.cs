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
using ServiceXpert.Infrastructure.SecurityModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceXpert.Infrastructure.Services;
public class AspNetUserService : ISecurityUserService
{
    private readonly UserManager<AspNetUser> userManager;
    private readonly IConfiguration configuration;
    private readonly ServiceXpertConfiguration serviceXpertConfiguration;
    private readonly ISecurityProfileService aspNetUserProfileService;
    private readonly RoleManager<AspNetRole> roleManager;

    public AspNetUserService(
        UserManager<AspNetUser> userManager,
        IConfiguration configuration,
        IOptions<ServiceXpertConfiguration> options,
        ISecurityProfileService aspNetUserProfileService,
        RoleManager<AspNetRole> roleManager
        )
    {
        this.userManager = userManager;
        this.configuration = configuration;
        this.serviceXpertConfiguration = options.Value;
        this.aspNetUserProfileService = aspNetUserProfileService;
        this.roleManager = roleManager;
    }

    public async Task<ServiceResult<string>> LoginAsync(LoginUser login)
    {
        var user = await this.userManager.FindByNameAsync(login.UserName);

        if (user != null && await this.userManager.CheckPasswordAsync(user, login.Password))
        {
            var userRoles = await this.userManager.GetRolesAsync(user);

            if (!userRoles.Any())
            {
                return ServiceResult<string>.Fail(ServiceResultStatus.Unauthorized, ["No roles assigned!"]);
            }

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // Equivalent of  ClaimTypes.NameIdentifier
                new(JwtRegisteredClaimNames.UniqueName, user.UserName!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.serviceXpertConfiguration.JwtSecretKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
            (
                issuer: this.configuration["Jwt:Issuer"],
                audience: this.configuration["Jwt:Audience"],
                signingCredentials: signingCredentials,
                claims: claims,
                expires: DateTimeOffset.UtcNow.AddMinutes(Convert.ToInt16(this.configuration["Jwt:ExpiresInMinutes"])).UtcDateTime
            );

            return ServiceResult<string>.Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        return ServiceResult<string>.Fail(ServiceResultStatus.Unauthorized, ["Invalid username or password"]);
    }

    public async Task<ServiceResult<Guid>> RegisterAsync(RegisterUser register)
    {
        var result = await this.userManager.CreateAsync(new AspNetUser()
        {
            UserName = register.UserName,
            Email = register.Email
        }, register.Password);

        if (!result.Succeeded)
        {
            return ServiceResult<Guid>.Fail(ServiceResultStatus.ValidationError, result.Errors.Select(e => e.Description));
        }

        var aspNetUser = await this.userManager.FindByNameAsync(register.UserName);

        var config = new TypeAdapterConfig();
        config.ForType<RegisterUser, CreateSecurityProfileDataObject>()
              .MapToConstructor(true)
              .ConstructUsing(src => new CreateSecurityProfileDataObject(aspNetUser!.Id));

        var aspNetUserProfile = register.Adapt<CreateSecurityProfileDataObject>(config);
        await this.aspNetUserProfileService.CreateAsync(aspNetUserProfile);

        return ServiceResult<Guid>.Ok(aspNetUser!.Id);
    }

    public async Task<ServiceResult> AssignRoleAsync(UserRole userRole)
    {
        var user = await this.userManager.FindByNameAsync(userRole.UserName);

        if (user == null)
        {
            return ServiceResult.Fail(ServiceResultStatus.NotFound, ["User not found!"]);
        }

        if (!await this.roleManager.RoleExistsAsync(userRole.RoleName))
        {
            return ServiceResult.Fail(ServiceResultStatus.NotFound, ["Role not found!"]);
        }

        if (await this.userManager.IsInRoleAsync(user, userRole.RoleName))
        {
            var errorMsg = $"User {userRole.UserName} is already assigned with role: {userRole.RoleName}!";
            return ServiceResult.Fail(ServiceResultStatus.ValidationError, [errorMsg]);
        }

        var result = await this.userManager.AddToRoleAsync(user, userRole.RoleName);
        return result.Succeeded
            ? ServiceResult.Ok()
            : ServiceResult.Fail(ServiceResultStatus.InternalError, result.Errors.Select(e => e.Description));
    }
}
