using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities;

namespace ServiceXpert.Infrastructure.DbContexts;
internal class AspNetUserProfileDbContext : DbContextBase, IEntityTypeConfiguration<AspNetUserProfile>
{
    public void Configure(EntityTypeBuilder<AspNetUserProfile> profile)
    {
        profile.HasKey(p => p.Id).IsClustered(false);
        profile.Property(p => p.FirstName).HasColumnType(ToVarcharColumn(256));
        profile.Property(p => p.LastName).HasColumnType(ToVarcharColumn(256));
    }
}
