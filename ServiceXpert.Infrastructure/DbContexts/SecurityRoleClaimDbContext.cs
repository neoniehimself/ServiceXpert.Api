using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities.Security;

namespace ServiceXpert.Infrastructure.DbContexts;
internal class SecurityRoleClaimDbContext : IEntityTypeConfiguration<SecurityRoleClaim>
{
    public void Configure(EntityTypeBuilder<SecurityRoleClaim> roleClaim)
    {
        roleClaim.ToTable(nameof(SecurityRoleClaim));
    }
}
