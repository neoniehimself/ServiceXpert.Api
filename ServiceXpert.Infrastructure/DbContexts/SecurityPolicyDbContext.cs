using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities.Security;

namespace ServiceXpert.Infrastructure.DbContexts;
internal class SecurityPolicyDbContext : DbContextBase, IEntityTypeConfiguration<SecurityPolicy>
{
    private readonly DateTimeOffset dateTimeOffset = new(2025, 10, 20, 0, 0, 0, 0, TimeSpan.Zero);

    public void Configure(EntityTypeBuilder<SecurityPolicy> policy)
    {
        policy.ToTable(nameof(SecurityPolicy));
        policy.HasKey(i => i.Id).IsClustered();
        policy.Property(i => i.Name).HasMaxLength(256);
        policy.HasData(
            new SecurityPolicy()
            {
                Id = (int)Domain.Enums.Security.SecurityPolicy.AdminOnly,
                Name = "AdminOnly",
                CreatedDate = this.dateTimeOffset,
                ModifiedDate = this.dateTimeOffset
            },
            new SecurityPolicy()
            {
                Id = (int)Domain.Enums.Security.SecurityPolicy.UserOnly,
                Name = "UserOnly",
                CreatedDate = this.dateTimeOffset,
                ModifiedDate = this.dateTimeOffset
            }
        );
    }
}
