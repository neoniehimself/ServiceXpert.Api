using Mapster;
using Microsoft.Extensions.DependencyInjection;
using ServiceXpert.Application.Services;
using ServiceXpert.Application.Services.Contracts;

namespace ServiceXpert.Application;
public static class ServiceContainer
{
    public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
    {
        // Mapster Configurations
        // IssueMapsterConfiguration.Map();

        // Mapster
        services.AddMapster();

        // Services
        services.AddScoped<IIssueService, IssueService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IAspNetUserProfileService, AspNetUserProfileService>();

        return services;
    }
}
