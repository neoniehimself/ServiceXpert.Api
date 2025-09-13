using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Infrastructure.AuthModels;

namespace ServiceXpert.Infrastructure.DbContexts;
internal class AspNetRoleDbContext : IEntityTypeConfiguration<AspNetRole>
{
    private readonly DateTime dateTime = new(2025, 9, 12, 0, 0, 0, 0, DateTimeKind.Utc);

    public void Configure(EntityTypeBuilder<AspNetRole> role)
    {
        role.HasData(
            new AspNetRole
            {
                Id = Guid.Parse("{2B954289-7678-4BD1-A3A3-171EB48346B2}"),
                Name = "Admin",
                NormalizedName = "ADMIN",
                CreatedDate = this.dateTime,
            }
        );
    }
}
