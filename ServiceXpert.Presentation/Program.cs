using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Rewrite;
using ServiceXpert.Application;
using ServiceXpert.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddOptions<SxpConfiguration>()
    .Bind(builder.Configuration.GetSection(nameof(SxpConfiguration)))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddApplicationServices()
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

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRewriter(new RewriteOptions().AddRedirect("(.*)/$", "$1", 301));

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
