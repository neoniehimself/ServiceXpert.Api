using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ServiceXpert.Application.Services;
using ServiceXpert.Application.Services.Contracts;

namespace ServiceXpert.Application;
public static class ServiceContainer
{
    public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
    {
        services.AddMapster();

        services.TryAddScoped<IIssueService, IssueService>();

        return services;
    }
}
