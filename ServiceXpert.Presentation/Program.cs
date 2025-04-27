using ServiceXpert.Application;
using ServiceXpert.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationLayerServices().AddInfrastructureLayerServices();

builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
