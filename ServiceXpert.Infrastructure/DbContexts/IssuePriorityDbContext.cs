using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities;
using DomainEnums = ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Infrastructure.DbContexts;
internal class IssuePriorityDbContext : DbContextBase, IEntityTypeConfiguration<IssuePriority>
{
    private readonly DateTimeOffset dateTimeOffset = new(2025, 3, 3, 0, 0, 0, 0, TimeSpan.Zero);

    public void Configure(EntityTypeBuilder<IssuePriority> issuePriority)
    {
        issuePriority.ToTable("IssuePriorities");
        issuePriority.HasKey(i => i.Id).IsClustered();
        issuePriority.Property(i => i.Name).HasMaxLength(64);
        issuePriority.HasData(
            new IssuePriority()
            {
                Id = (int)DomainEnums.IssuePriority.Outage,
                Name = "Outage",
                CreatedDate = this.dateTimeOffset,
                ModifiedDate = this.dateTimeOffset
            },
            new IssuePriority()
            {
                Id = (int)DomainEnums.IssuePriority.Critical,
                Name = "Critical",
                CreatedDate = this.dateTimeOffset,
                ModifiedDate = this.dateTimeOffset
            },
            new IssuePriority()
            {
                Id = (int)DomainEnums.IssuePriority.High,
                Name = "High",
                CreatedDate = this.dateTimeOffset,
                ModifiedDate = this.dateTimeOffset
            },
            new IssuePriority()
            {
                Id = (int)DomainEnums.IssuePriority.Medium,
                Name = "Medium",
                CreatedDate = this.dateTimeOffset,
                ModifiedDate = this.dateTimeOffset
            },
            new IssuePriority()
            {
                Id = (int)DomainEnums.IssuePriority.Low,
                Name = "Low",
                CreatedDate = this.dateTimeOffset,
                ModifiedDate = this.dateTimeOffset
            }
        );
    }
}
