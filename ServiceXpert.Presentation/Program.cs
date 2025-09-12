using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using ServiceXpert.Application;
using ServiceXpert.Domain.Shared.Enums;
using ServiceXpert.Infrastructure;
using ServiceXpert.Infrastructure.AuthModels;
using ServiceXpert.Infrastructure.DbContexts;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddApplicationLayerServices()
    .AddInfrastructureLayerServices();

#region Authentication & Authorization Service Configurations
builder.Services
    .AddIdentity<AspNetUser, AspNetRole>()
    .AddEntityFrameworkStores<SxpDbContext>()
    .AddDefaultTokenProviders();

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                Environment.GetEnvironmentVariable("ServiceXpert_JwtKey", EnvironmentVariableTarget.Machine) ?? throw new KeyNotFoundException("Fatal: Missing Jwt Key")))
        };
    });

var authBuilder = builder.Services.AddAuthorizationBuilder();
authBuilder.AddPolicy(nameof(Policy.Admin), policy => policy.RequireRole(nameof(Role.Admin)));
authBuilder.AddPolicy(nameof(Policy.User), policy => policy.RequireRole(nameof(Role.User)));

#endregion

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
