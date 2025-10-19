using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities.Security;

namespace ServiceXpert.Infrastructure.DbContexts;
internal class SecurityUserRoleDbContext : IEntityTypeConfiguration<SecurityUserRole>
{
    private readonly DateTimeOffset dateTimeOffset = new(2025, 9, 12, 0, 0, 0, 0, TimeSpan.Zero);

    public void Configure(EntityTypeBuilder<SecurityUserRole> userRole)
    {
        userRole.ToTable(nameof(SecurityUserRole));
        userRole.HasData(
            new SecurityUserRole
            {
                UserId = Guid.Parse("{E45ACEFA-74B0-4F28-B81F-FBC02D9778B5}"),
                RoleId = Guid.Parse("{2B954289-7678-4BD1-A3A3-171EB48346B2}"),
                CreatedDate = this.dateTimeOffset,
            }
        );
    }
}
