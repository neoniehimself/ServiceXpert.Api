using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities.Issues;

namespace ServiceXpert.Infrastructure.DbContexts;
internal class IssuePriorityDbContext : DbContextBase, IEntityTypeConfiguration<IssuePriority>
{
    private readonly DateTimeOffset dateTimeOffset = new(2025, 3, 3, 0, 0, 0, 0, TimeSpan.Zero);

    public void Configure(EntityTypeBuilder<IssuePriority> priority)
    {
        priority.ToTable(nameof(IssuePriority));
        priority.HasKey(i => i.Id).IsClustered();
        priority.Property(i => i.Name).HasMaxLength(64);
        priority.HasData(
            new IssuePriority()
            {
                Id = (int)Domain.Enums.Issues.IssuePriority.Outage,
                Name = "Outage",
                CreatedDate = this.dateTimeOffset,
                ModifiedDate = this.dateTimeOffset
            },
            new IssuePriority()
            {
                Id = (int)Domain.Enums.Issues.IssuePriority.Critical,
                Name = "Critical",
                CreatedDate = this.dateTimeOffset,
                ModifiedDate = this.dateTimeOffset
            },
            new IssuePriority()
            {
                Id = (int)Domain.Enums.Issues.IssuePriority.High,
                Name = "High",
                CreatedDate = this.dateTimeOffset,
                ModifiedDate = this.dateTimeOffset
            },
            new IssuePriority()
            {
                Id = (int)Domain.Enums.Issues.IssuePriority.Medium,
                Name = "Medium",
                CreatedDate = this.dateTimeOffset,
                ModifiedDate = this.dateTimeOffset
            },
            new IssuePriority()
            {
                Id = (int)Domain.Enums.Issues.IssuePriority.Low,
                Name = "Low",
                CreatedDate = this.dateTimeOffset,
                ModifiedDate = this.dateTimeOffset
            }
        );
    }
}
