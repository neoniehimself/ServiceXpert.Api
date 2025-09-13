using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using ServiceXpert.Application;
using ServiceXpert.Infrastructure;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddOptions<ServiceXpertConfiguration>()
    .Bind(builder.Configuration.GetSection(nameof(ServiceXpertConfiguration)))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services
    .AddApplicationLayerServices()
    .AddInfrastructureLayerServices();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration.GetSection(
                nameof(ServiceXpertConfiguration)).Get<ServiceXpertConfiguration>()!.JwtSecretKey)
        )
    };
});

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
