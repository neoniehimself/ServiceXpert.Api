using Microsoft.Extensions.DependencyInjection;
using ServiceXpert.Domain.Repositories.Contracts;
using ServiceXpert.Infrastructure.DbContexts;
using ServiceXpert.Infrastructure.Repositories;

namespace ServiceXpert.Infrastructure;
public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services)
    {
        services.AddDbContext<SxpDbContext>();
        services.AddScoped<IIssueRepository, IssueRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IAspNetUserProfileRepository, AspNetUserProfileRepository>();

        return services;
    }
}
