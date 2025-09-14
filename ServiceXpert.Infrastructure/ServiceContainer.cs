using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Domain.Repositories;
using ServiceXpert.Domain.Shared.Enums;
using ServiceXpert.Infrastructure.DbContexts;
using ServiceXpert.Infrastructure.Repositories;
using ServiceXpert.Infrastructure.SecurityModels;
using ServiceXpert.Infrastructure.Services;
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
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IAspNetUserProfileRepository, AspNetUserProfileRepository>();

        // Services
        services.AddScoped<IAspNetUserService, AspNetUserService>();
        services.AddScoped<IAspNetRoleService, AspNetRoleService>();

        #region Configure Identity, Authentication and Authorization
        services.AddHttpContextAccessor();

        services
            .AddIdentity<AspNetUser, AspNetRole>()
            .AddEntityFrameworkStores<SxpDbContext>()
            .AddDefaultTokenProviders();

        services
            .AddAuthentication(options =>
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
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration.GetSection(
                            nameof(ServiceXpertConfiguration)).Get<ServiceXpertConfiguration>()!.JwtSecretKey)
                    )
                };
            });

        var authBuilder = services.AddAuthorizationBuilder();
        authBuilder.AddPolicy(nameof(Policy.Admin), policy => policy.RequireRole(nameof(Role.Admin)));
        authBuilder.AddPolicy(nameof(Policy.User), policy => policy.RequireRole(nameof(Role.Admin), nameof(Role.User)));
        #endregion

        return services;
    }
}
