using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Infrastructure.SecurityModels;

namespace ServiceXpert.Infrastructure.DbContexts;
internal class AspNetUserDbContext : IEntityTypeConfiguration<AspNetUser>
{
    private readonly DateTime dateTime = new(2025, 9, 12, 0, 0, 0, 0, DateTimeKind.Utc);

    public void Configure(EntityTypeBuilder<AspNetUser> user)
    {
        user.HasOne<AspNetUserProfile>().WithOne().HasForeignKey<AspNetUserProfile>(p => p.Id);
        user.HasData(new AspNetUser
        {
            Id = Guid.Parse("{E45ACEFA-74B0-4F28-B81F-FBC02D9778B5}"),
            UserName = "admin",
            PasswordHash = "AQAAAAIAAYagAAAAEA8/ykwd1Q9T24ymPfU/+GTDJfzc81J27jOD+oMtfBQPHISby25dFCc9ZTbIvgVOUg==",
            NormalizedUserName = "ADMIN",
            Email = "noemail@noemail.com",
            NormalizedEmail = "NOEMAIL@NOEMAIL.COM",
            ConcurrencyStamp = "CD774732-9643-4427-A745-B72B3EAB125D",
            CreatedDate = this.dateTime,
        });
    }
}
