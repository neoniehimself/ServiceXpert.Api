using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Options;
using ServiceXpert.Domain.Audits;
using ServiceXpert.Domain.Entities.Issues;
using ServiceXpert.Domain.Entities.Security;
using ServiceXpert.Infrastructure.SecurityModels;
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
    private readonly ServiceXpertConfiguration serviceXpertConfiguration;
    private readonly IHttpContextAccessor httpContextAccessor;

    public DbSet<Issue> Issues { get; set; }

    public DbSet<IssueComment> Comments { get; set; }

    public DbSet<SecurityProfile> AspNetUserProfiles { get; set; }

    public SxpDbContext(IOptions<ServiceXpertConfiguration> options, IHttpContextAccessor httpContextAccessor)
    {
        this.serviceXpertConfiguration = options.Value;
        this.httpContextAccessor = httpContextAccessor;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder
            .UseSqlServer(this.serviceXpertConfiguration.ConnectionString, sqlServerOptionsAction =>
                sqlServerOptionsAction.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Conventions.Remove(typeof(ForeignKeyIndexConvention));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = this.ChangeTracker.Entries<IAudit>();
        var userId = Guid.Parse(this.httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var dateTimeOffset = DateTimeOffset.UtcNow;

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
                    entry.Entity.CreatedByUserId = userId;
                    entry.Entity.CreatedDate = dateTimeOffset;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedByUserId = userId;
                    entry.Entity.ModifiedDate = dateTimeOffset;
                    break;
                default:
                    continue;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
