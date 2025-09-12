using Microsoft.Extensions.DependencyInjection;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Domain.Repositories.Contracts;
using ServiceXpert.Infrastructure.DbContexts;
using ServiceXpert.Infrastructure.Repositories;
using ServiceXpert.Infrastructure.Services;

namespace ServiceXpert.Infrastructure;
public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services)
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

        return services;
    }
}
