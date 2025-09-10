using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities;

namespace ServiceXpert.Infrastructure.DbContexts;
internal class AspNetUserProfileDbContext : DbContextBase, IEntityTypeConfiguration<AspNetUserProfile>
{
    public void Configure(EntityTypeBuilder<AspNetUserProfile> profile)
    {
        profile.HasKey(p => p.Id).IsClustered(false);
        profile.Property(p => p.FirstName).HasMaxLength(256);
        profile.Property(p => p.LastName).HasMaxLength(256);
    }
}
