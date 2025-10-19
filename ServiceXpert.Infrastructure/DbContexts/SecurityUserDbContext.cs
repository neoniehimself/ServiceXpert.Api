using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities.Security;

namespace ServiceXpert.Infrastructure.DbContexts;
internal class SecurityUserDbContext : IEntityTypeConfiguration<SecurityUser>
{
    private readonly DateTimeOffset dateTimeOffset = new(2025, 9, 12, 0, 0, 0, 0, TimeSpan.Zero);

    public void Configure(EntityTypeBuilder<SecurityUser> user)
    {
        user.ToTable(nameof(SecurityUser));
        user.HasOne<SecurityProfile>().WithOne().HasForeignKey<SecurityProfile>(p => p.Id);
        user.HasData(new SecurityUser
        {
            Id = Guid.Parse("{E45ACEFA-74B0-4F28-B81F-FBC02D9778B5}"),
            UserName = "admin",
            PasswordHash = "AQAAAAIAAYagAAAAEA8/ykwd1Q9T24ymPfU/+GTDJfzc81J27jOD+oMtfBQPHISby25dFCc9ZTbIvgVOUg==",
            NormalizedUserName = "ADMIN",
            Email = "noemail@noemail.com",
            NormalizedEmail = "NOEMAIL@NOEMAIL.COM",
            ConcurrencyStamp = "CD774732-9643-4427-A745-B72B3EAB125D",
            CreatedDate = this.dateTimeOffset,
        });
    }
}
