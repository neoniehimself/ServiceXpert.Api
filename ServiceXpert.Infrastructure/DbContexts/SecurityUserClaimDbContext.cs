using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities.Security;

namespace ServiceXpert.Infrastructure.DbContexts;
internal class SecurityUserClaimDbContext : IEntityTypeConfiguration<SecurityUserClaim>
{
    public void Configure(EntityTypeBuilder<SecurityUserClaim> userClaim)
    {
        userClaim.ToTable(nameof(SecurityUserClaim));
    }
}
