using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ServiceXpert.Application.Services.Contracts.Security;
using ServiceXpert.Domain.Entities.Security;
using ServiceXpert.Domain.Repositories.Issues;
using ServiceXpert.Domain.Repositories.Security;
using ServiceXpert.Infrastructure.DbContexts;
using ServiceXpert.Infrastructure.Repositories.Issues;
using ServiceXpert.Infrastructure.Repositories.Security;
using ServiceXpert.Infrastructure.Services.Security;
using System.Text;

namespace ServiceXpert.Infrastructure;
public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<SxpDbContext>();

        // Repositories
        services.AddScoped<IIssueRepository, IssueRepository>();
        services.AddScoped<IIssueCommentRepository, IssueCommentRepository>();
        services.AddScoped<ISecurityProfileRepository, SecurityProfileRepository>();

        // Services
        services.AddScoped<ISecurityUserService, SecurityUserService>();

        #region Configure Identity, Authentication and Authorization
        services.AddHttpContextAccessor();

        services.AddIdentity<SecurityUser, SecurityRole>()
            .AddEntityFrameworkStores<SxpDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidIssuer = configuration["Jwt:Issuer"],
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection(
                        nameof(SxpConfiguration)).Get<SxpConfiguration>()!.JwtSecretKey)
                )
            };
        });

        var authBuilder = services.AddAuthorizationBuilder();

        authBuilder.AddPolicy(nameof(Domain.Enums.Security.SecurityPolicy.AdminOnly),
            policy => policy.RequireRole(nameof(Domain.Enums.Security.SecurityRole.Admin)));

        authBuilder.AddPolicy(nameof(Domain.Enums.Security.SecurityPolicy.UserOnly),
            policy => policy.RequireRole(nameof(Domain.Enums.Security.SecurityRole.User)));
        #endregion

        return services;
    }
}
