using Mapster;
using Microsoft.Extensions.DependencyInjection;
using ServiceXpert.Application.Services.Concretes.Issues;
using ServiceXpert.Application.Services.Concretes.Security;
using ServiceXpert.Application.Services.Contracts.Issues;
using ServiceXpert.Application.Services.Contracts.Security;

namespace ServiceXpert.Application;
public static class ServiceContainer
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Mapster
        services.AddMapster();

        // Services
        services.AddScoped<IIssueService, IssueService>();
        services.AddScoped<IIssueCommentService, IssueCommentService>();
        services.AddScoped<ISecurityProfileService, SecurityProfileService>();

        return services;
    }
}
