using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities.Security;

namespace ServiceXpert.Infrastructure.DbContexts;
internal class SecurityUserTokenDbContext : DbContextBase, IEntityTypeConfiguration<SecurityUserToken>
{
    public void Configure(EntityTypeBuilder<SecurityUserToken> userToken)
    {
        userToken.ToTable(nameof(SecurityUserToken));
        userToken.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name }).IsClustered(false);
    }
}
