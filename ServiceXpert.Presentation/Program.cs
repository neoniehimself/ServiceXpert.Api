using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ServiceXpert.Application;
using ServiceXpert.Infrastructure;
using ServiceXpert.Infrastructure.AuthModels;
using ServiceXpert.Infrastructure.DbContexts;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddApplicationLayerServices()
    .AddInfrastructureLayerServices();

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
                Environment.GetEnvironmentVariable("ServiceXpert_JwtKey", EnvironmentVariableTarget.Machine) ?? throw new KeyNotFoundException("Fatal: Missing JWT Key")))
        };
    });

var authBuilder = builder.Services.AddAuthorizationBuilder();
authBuilder.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
authBuilder.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));

builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true);

builder.Services.Configure<RouteOptions>(options => options.AppendTrailingSlash = true);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
