using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Shared.Audits;
using ServiceXpert.Infrastructure.AuthModels;
using System.Reflection;
using System.Security.Claims;

namespace ServiceXpert.Infrastructure.DbContexts;
public class SxpDbContext : IdentityDbContext<
    AspNetUser,
    AspNetRole,
    Guid,
    IdentityUserClaim<Guid>,
    AspNetUserRole,
    IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>,
    IdentityUserToken<Guid>>
{
    private readonly IHttpContextAccessor httpContextAccessor;

    private static string ConnectionString
    {
        get
        {
            string? connectionString = Environment.GetEnvironmentVariable("ServiceXpert_ConnectionString", EnvironmentVariableTarget.Machine);
            return connectionString ?? throw new KeyNotFoundException("Fatal: Missing connection string");
        }
    }

    public DbSet<Issue> Issues { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<AspNetUserProfile> AspNetUserProfiles { get; set; }

    public SxpDbContext(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

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

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = this.ChangeTracker.Entries<IAuditable>();
        var userId = Guid.Parse(this.httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var dateTimeUtcNow = DateTime.UtcNow;

        // Used for debugging purposes
        /*var claims = this.httpContextAccessor.HttpContext?.User?.Claims;
        foreach (var claim in claims!)
        {
            Console.WriteLine($"{claim.Type} = {claim.Value}");
        }*/

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreateUserId = userId;
                    entry.Entity.CreateDate = dateTimeUtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifyUserId = userId;
                    entry.Entity.ModifyDate = dateTimeUtcNow;
                    break;
                default:
                    continue;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
