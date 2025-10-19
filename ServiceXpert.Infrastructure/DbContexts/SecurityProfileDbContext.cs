using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities.Security;

namespace ServiceXpert.Infrastructure.DbContexts;
internal class SecurityProfileDbContext : DbContextBase, IEntityTypeConfiguration<SecurityProfile>
{
    private readonly DateTimeOffset dateTimeOffset = new(2025, 9, 12, 0, 0, 0, 0, TimeSpan.Zero);

    public void Configure(EntityTypeBuilder<SecurityProfile> profile)
    {
        profile.ToTable(nameof(SecurityProfile));
        profile.HasKey(p => p.Id).IsClustered(false);
        profile.Property(p => p.FirstName).HasMaxLength(256);
        profile.Property(p => p.LastName).HasMaxLength(256);

        profile.HasData(
            new SecurityProfile
            {
                Id = Guid.Parse("{E45ACEFA-74B0-4F28-B81F-FBC02D9778B5}"),
                FirstName = "Admin",
                LastName = "Admin",
                CreatedDate = this.dateTimeOffset,
            }
        );
    }
}
