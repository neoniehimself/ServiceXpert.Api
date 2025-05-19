using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ServiceXpert.Application.Mapping;
using ServiceXpert.Application.Services;
using ServiceXpert.Application.Services.Contracts;

namespace ServiceXpert.Application;
public static class ServiceContainer
{
    public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
    {
        IssueMapsterConfiguration.Map();

        services.AddMapster();

        services.TryAddScoped<IIssueService, IssueService>();
        services.TryAddScoped<ICommentService, CommentService>();

        return services;
    }
}
