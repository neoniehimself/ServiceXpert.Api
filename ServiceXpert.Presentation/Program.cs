using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using ServiceXpert.Application;
using ServiceXpert.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddOptions<ServiceXpertConfiguration>()
    .Bind(builder.Configuration.GetSection(nameof(ServiceXpertConfiguration)))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;

    // Global Authorization Filter
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    options.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.Configure<RouteOptions>(options => options.AppendTrailingSlash = true);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
