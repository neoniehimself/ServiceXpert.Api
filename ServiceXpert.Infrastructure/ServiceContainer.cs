using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ServiceXpert.Domain.Repositories.Contracts;
using ServiceXpert.Infrastructure.Repositories;
using ServiceXpert.ServiceXpert.Infrastructure.DbContexts;

namespace ServiceXpert.Infrastructure;
public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services)
    {
        services.AddDbContext<SxpDbContext>();
        services.TryAddScoped<IIssueRepository, IssueRepository>();
        services.TryAddScoped<ICommentRepository, CommentRepository>();

        return services;
    }
}
