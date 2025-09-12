using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities;

namespace ServiceXpert.Infrastructure.DbContexts;
internal class AspNetUserProfileDbContext : DbContextBase, IEntityTypeConfiguration<AspNetUserProfile>
{
    private readonly DateTime dateTime = new(2025, 9, 12, 0, 0, 0, 0, DateTimeKind.Utc);

    public void Configure(EntityTypeBuilder<AspNetUserProfile> profile)
    {
        profile.HasKey(p => p.Id).IsClustered(false);
        profile.Property(p => p.FirstName).HasMaxLength(256);
        profile.Property(p => p.LastName).HasMaxLength(256);

        profile.HasData(
            new AspNetUserProfile
            {
                Id = Guid.Parse("{E45ACEFA-74B0-4F28-B81F-FBC02D9778B5}"),
                FirstName = "Admin",
                LastName = "Admin",
                CreateDate = this.dateTime,
                ModifyDate = this.dateTime
            }
        );
    }
}
