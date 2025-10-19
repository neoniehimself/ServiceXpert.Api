using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Enums.Security;
using ServiceXpert.Infrastructure.SecurityModels;

namespace ServiceXpert.Infrastructure.DbContexts;
internal class AspNetRoleDbContext : IEntityTypeConfiguration<AspNetRole>
{
    private readonly DateTimeOffset dateTimeOffset = new(2025, 9, 12, 0, 0, 0, 0, TimeSpan.Zero);

    public void Configure(EntityTypeBuilder<AspNetRole> role)
    {
        role.HasData(
            new AspNetRole
            {
                Id = Guid.Parse("{2B954289-7678-4BD1-A3A3-171EB48346B2}"),
                Name = nameof(SecurityRole.Admin),
                NormalizedName = "ADMIN",
                CreatedDate = this.dateTimeOffset,
            },
            new AspNetRole
            {
                Id = Guid.Parse("{9EA822F4-CFB5-41B2-A0B9-26D65E57261F}"),
                Name = nameof(SecurityRole.User),
                NormalizedName = "USER",
                CreatedDate = this.dateTimeOffset,
            }
        );
    }
}
