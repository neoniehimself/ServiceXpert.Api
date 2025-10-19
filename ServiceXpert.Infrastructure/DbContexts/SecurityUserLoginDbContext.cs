using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities.Security;

namespace ServiceXpert.Infrastructure.DbContexts;
internal class SecurityUserLoginDbContext : IEntityTypeConfiguration<SecurityUserLogin>
{
    public void Configure(EntityTypeBuilder<SecurityUserLogin> userLogin)
    {
        userLogin.ToTable(nameof(SecurityUserLogin));
    }
}
