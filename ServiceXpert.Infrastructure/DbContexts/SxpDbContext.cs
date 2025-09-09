using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Infrastructure.Models;
using System.Reflection;

namespace ServiceXpert.Infrastructure.DbContexts;
public class SxpDbContext : IdentityDbContext<AspNetUser, AspNetRole, Guid, IdentityUserClaim<Guid>,
        AspNetUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    private static string ConnectionString
    {
        get
        {
            string? connectionString = Environment.GetEnvironmentVariable("ServiceXpert", EnvironmentVariableTarget.Machine);
            return connectionString ?? throw new KeyNotFoundException("Fatal: Missing connection string");
        }
    }

    public DbSet<Issue> Issue { get; set; }

    public DbSet<Comment> Comment { get; set; }

    public DbSet<AspNetUserProfile> AspNetUserProfiles { get; set; }

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
