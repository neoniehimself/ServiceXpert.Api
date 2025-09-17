using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServiceXpert.Application.DataObjects.AspNetUserProfile;
using ServiceXpert.Application.DataObjects.Security;
using ServiceXpert.Application.Services.Contracts;
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

    public async Task<(bool Succeeded, IEnumerable<string> Errors, string token)> LoginAsync(LoginUserDataObject loginUser)
    {
        var user = await this.userManager.FindByNameAsync(loginUser.UserName);

        if (user != null && await this.userManager.CheckPasswordAsync(user, loginUser.Password))
        {
            var userRoles = await this.userManager.GetRolesAsync(user);

            if (!userRoles.Any())
            {
                return (false, new List<string> { "User has no roles assigned!" }, string.Empty);
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

            return (true, [], new JwtSecurityTokenHandler().WriteToken(token));
        }

        return (false, new List<string> { "Invalid username or password!" }, string.Empty);
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors, Guid aspNetUserId)> RegisterAsync(RegisterUserDataObject registerUser)
    {
        var result = await this.userManager.CreateAsync(new AspNetUser()
        {
            UserName = registerUser.UserName,
            Email = registerUser.Email
        }, registerUser.Password);

        if (!result.Succeeded)
        {
            return (false, result.Errors.Select(e => e.Description), Guid.Empty);
        }

        var aspNetUser = await this.userManager.FindByNameAsync(registerUser.UserName);

        var config = new TypeAdapterConfig();
        config.ForType<RegisterUserDataObject, AspNetUserProfileDataObjectForCreate>()
              .MapToConstructor(true)
              .ConstructUsing(src => new AspNetUserProfileDataObjectForCreate(aspNetUser!.Id));

        var aspNetUserProfile = registerUser.Adapt<AspNetUserProfileDataObjectForCreate>(config);
        await this.aspNetUserProfileService.CreateAsync(aspNetUserProfile);

        return (true, [], aspNetUser!.Id);
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> AssignRoleAsync(UserRoleDataObject userRole)
    {
        var user = await this.userManager.FindByNameAsync(userRole.UserName);

        if (user == null)
        {
            return (false, new List<string> { "User not found!" });
        }

        if (!await this.roleManager.RoleExistsAsync(userRole.RoleName))
        {
            return (false, new List<string> { "Role not found!" });
        }

        if (await this.userManager.IsInRoleAsync(user, userRole.RoleName))
        {
            return (false, new List<string> { "User already assigned to this role!" });
        }

        var result = await this.userManager.AddToRoleAsync(user, userRole.RoleName);
        return result.Succeeded ? (true, []) : (false, result.Errors.Select(e => e.Description));
    }
}
