using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServiceXpert.Application.DataObjects.AspNetUserProfile;
using ServiceXpert.Application.DataObjects.Security;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Application.Shared;
using ServiceXpert.Application.Shared.Enums;
using ServiceXpert.Infrastructure.SecurityModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceXpert.Infrastructure.Services;
public class AspNetUserService : IAspNetUserService
{
    private readonly UserManager<AspNetUser> userManager;
    private readonly IConfiguration configuration;
    private readonly ServiceXpertConfiguration serviceXpertConfiguration;
    private readonly IAspNetUserProfileService aspNetUserProfileService;
    private readonly RoleManager<AspNetRole> roleManager;

    public AspNetUserService(
        UserManager<AspNetUser> userManager,
        IConfiguration configuration,
        IOptions<ServiceXpertConfiguration> options,
        IAspNetUserProfileService aspNetUserProfileService,
        RoleManager<AspNetRole> roleManager
        )
    {
        this.userManager = userManager;
        this.configuration = configuration;
        this.serviceXpertConfiguration = options.Value;
        this.aspNetUserProfileService = aspNetUserProfileService;
        this.roleManager = roleManager;
    }

    public async Task<Result<string>> LoginAsync(LoginDataObject login)
    {
        var user = await this.userManager.FindByNameAsync(login.UserName);

        if (user != null && await this.userManager.CheckPasswordAsync(user, login.Password))
        {
            var userRoles = await this.userManager.GetRolesAsync(user);

            if (!userRoles.Any())
            {
                return Result<string>.Fail(ResultStatus.Unauthorized, "No roles assigned!");
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

            return Result<string>.Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        return Result<string>.Fail(ResultStatus.Unauthorized, "Invalid username or password");
    }

    public async Task<Result<Guid>> RegisterAsync(RegisterDataObject register)
    {
        var result = await this.userManager.CreateAsync(new AspNetUser()
        {
            UserName = register.UserName,
            Email = register.Email
        }, register.Password);

        if (!result.Succeeded)
        {
            return Result<Guid>.Fail(ResultStatus.InternalError, result.Errors.Select(e => e.Description));
        }

        var aspNetUser = await this.userManager.FindByNameAsync(register.UserName);

        var config = new TypeAdapterConfig();
        config.ForType<RegisterDataObject, AspNetUserProfileDataObjectForCreate>()
              .MapToConstructor(true)
              .ConstructUsing(src => new AspNetUserProfileDataObjectForCreate(aspNetUser!.Id));

        var aspNetUserProfile = register.Adapt<AspNetUserProfileDataObjectForCreate>(config);
        await this.aspNetUserProfileService.CreateAsync(aspNetUserProfile);

        return Result<Guid>.Ok(aspNetUser!.Id);
    }

    public async Task<Result> AssignRoleAsync(UserRoleDataObject userRole)
    {
        var user = await this.userManager.FindByNameAsync(userRole.UserName);

        if (user == null)
        {
            return Result.Fail(ResultStatus.NotFound, "User not found!");
        }

        if (!await this.roleManager.RoleExistsAsync(userRole.RoleName))
        {
            return Result.Fail(ResultStatus.NotFound, "Role not found!");
        }

        if (await this.userManager.IsInRoleAsync(user, userRole.RoleName))
        {
            return Result.Fail(ResultStatus.ValidationError, "User " + userRole.UserName + " is already assigned with role: " + userRole.RoleName);
        }

        var result = await this.userManager.AddToRoleAsync(user, userRole.RoleName);
        return result.Succeeded
            ? Result.Ok()
            : Result.Fail(ResultStatus.InternalError, result.Errors.Select(e => e.Description));
    }
}
