using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities.Issues;

namespace ServiceXpert.Infrastructure.DbContexts;
internal class IssueStatusDbContext : DbContextBase, IEntityTypeConfiguration<IssueStatus>
{
    private readonly DateTimeOffset dateTimeOffset = new(2025, 2, 24, 0, 0, 0, 0, TimeSpan.Zero);

    public void Configure(EntityTypeBuilder<IssueStatus> status)
    {
        status.ToTable(nameof(IssueStatus));
        status.HasKey(i => i.Id).IsClustered();
        status.Property(i => i.Name).HasMaxLength(64);
        status.HasData(
            new IssueStatus()
            {
                Id = (int)Domain.Enums.Issues.IssueStatus.New,
                Name = "New",
                CreatedDate = this.dateTimeOffset,
                ModifiedDate = this.dateTimeOffset
            },
            new IssueStatus()
            {
                Id = (int)Domain.Enums.Issues.IssueStatus.ForAnalysis,
                Name = "For Analysis",
                CreatedDate = this.dateTimeOffset,
                ModifiedDate = this.dateTimeOffset
            },
            new IssueStatus()
            {
                Id = (int)Domain.Enums.Issues.IssueStatus.InProgress,
                Name = "In Progress",
                CreatedDate = this.dateTimeOffset,
                ModifiedDate = this.dateTimeOffset
            },
            new IssueStatus()
            {
                Id = (int)Domain.Enums.Issues.IssueStatus.Resolved,
                Name = "Resolved",
                CreatedDate = this.dateTimeOffset,
                ModifiedDate = this.dateTimeOffset
            },
            new IssueStatus()
            {
                Id = (int)Domain.Enums.Issues.IssueStatus.Closed,
                Name = "Closed",
                CreatedDate = this.dateTimeOffset,
                ModifiedDate = this.dateTimeOffset
            }
        );
    }
}
