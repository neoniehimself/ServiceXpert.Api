using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Domain.Repositories;
using ServiceXpert.Domain.Shared.Enums;
using ServiceXpert.Infrastructure.DbContexts;
using ServiceXpert.Infrastructure.Repositories;
using ServiceXpert.Infrastructure.SecurityModels;
using ServiceXpert.Infrastructure.Services;

namespace ServiceXpert.Infrastructure;
public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        // DbContext
        services.AddDbContext<SxpDbContext>();

        // Repositories
        services.AddScoped<IIssueRepository, IssueRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IAspNetUserProfileRepository, AspNetUserProfileRepository>();

        // Services
        services.AddScoped<IAspNetUserService, AspNetUserService>();
        services.AddScoped<IAspNetRoleService, AspNetRoleService>();

        services
            .AddIdentity<AspNetUser, AspNetRole>()
            .AddEntityFrameworkStores<SxpDbContext>()
            .AddDefaultTokenProviders();

        var authBuilder = services.AddAuthorizationBuilder();

        // Admin Policies
        authBuilder.AddPolicy(nameof(Policy.Admin), policy => policy.RequireRole(nameof(Role.Admin)));
        // User Policies
        authBuilder.AddPolicy(nameof(Policy.User), policy => policy.RequireRole(nameof(Role.Admin)));
        authBuilder.AddPolicy(nameof(Policy.User), policy => policy.RequireRole(nameof(Role.User)));

        return services;
    }
}
