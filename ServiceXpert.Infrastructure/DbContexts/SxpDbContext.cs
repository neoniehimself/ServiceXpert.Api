using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ServiceXpert.Domain.Entities;
using System.Reflection;

namespace ServiceXpert.ServiceXpert.Infrastructure.DbContexts;
public class SxpDbContext : DbContext
{
    private static string ConnectionString
    {
        get
        {
            string? connectionString = Environment.GetEnvironmentVariable("ServiceXpert", EnvironmentVariableTarget.User);
            return connectionString ?? throw new KeyNotFoundException("Fatal: Missing connection string");
        }
    }

    public DbSet<Issue> Issue { get; set; }

    public DbSet<Comment> Comment { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(ConnectionString, sqlServerOptionsAction =>
                sqlServerOptionsAction.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>().HaveColumnType("VARCHAR");
        configurationBuilder.Conventions.Remove(typeof(ForeignKeyIndexConvention));
        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        base.OnModelCreating(modelBuilder);
    }
}
