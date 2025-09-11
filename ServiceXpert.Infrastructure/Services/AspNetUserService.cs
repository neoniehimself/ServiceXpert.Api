using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Application.DataObjects.AspNetUserProfile;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Infrastructure.AuthModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ServiceXpert.Infrastructure.Services;
public class AspNetUserService : IAspNetUserService
{
    private readonly UserManager<AspNetUser> userManager;
    private readonly IAspNetUserProfileService aspNetUserProfileService;
    private readonly IConfiguration configuration;

    public AspNetUserService(
        UserManager<AspNetUser> userManager,
        IAspNetUserProfileService aspNetUserProfileService,
        IConfiguration configuration)
    {
        this.userManager = userManager;
        this.aspNetUserProfileService = aspNetUserProfileService;
        this.configuration = configuration;
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors, Guid aspNetUserId)> RegisterAsync(RegisterUserDataObject dataObject)
    {
        var result = await this.userManager.CreateAsync(new AspNetUser()
        {
            UserName = dataObject.UserName,
            Email = dataObject.Email
        }, dataObject.Password);

        if (!result.Succeeded)
        {
            return (false, result.Errors.Select(e => e.Description), Guid.Empty);
        }

        var aspNetUser = await this.userManager.FindByNameAsync(dataObject.UserName);

        var config = new TypeAdapterConfig();
        config.ForType<RegisterUserDataObject, AspNetUserProfileDataObjectForCreate>()
              .MapToConstructor(true)
              .ConstructUsing(src => new AspNetUserProfileDataObjectForCreate(aspNetUser!.Id));

        var aspNetUserProfile = dataObject.Adapt<AspNetUserProfileDataObjectForCreate>(config);
        await this.aspNetUserProfileService.CreateAsync(aspNetUserProfile);

        return (true, Enumerable.Empty<string>(), aspNetUser!.Id);
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors, string token)> LoginAsync(LoginUserDataObject dataObject)
    {
        var user = await this.userManager.FindByNameAsync(dataObject.UserName);

        if (user != null && await this.userManager.CheckPasswordAsync(user, dataObject.Password))
        {
            var userRoles = await this.userManager.GetRolesAsync(user);

            if (!userRoles.Any())
            {
                return (false, new List<string> { "User has no roles assigned!" }, string.Empty);
            }

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.UserName!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken
            (
                issuer: this.configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(this.configuration["Jwt:ExpiresInMinutes"])),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("ServiceXpert_JwtKey", EnvironmentVariableTarget.Machine)!)),
                        SecurityAlgorithms.HmacSha256
                    )
            );

            return (true, Enumerable.Empty<string>(), new JwtSecurityTokenHandler().WriteToken(token));
        }

        return (false, new List<string> { "Invalid username or password!" }, string.Empty);
    }
}
