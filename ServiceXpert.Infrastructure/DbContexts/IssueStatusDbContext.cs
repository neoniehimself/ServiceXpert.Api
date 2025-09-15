using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities;
using DomainEnums = ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Infrastructure.DbContexts;
internal class IssueStatusDbContext : DbContextBase, IEntityTypeConfiguration<IssueStatus>
{
    private readonly DateTimeOffset dateTimeOffset = new(2025, 2, 24, 0, 0, 0, 0, TimeSpan.Zero);

    public void Configure(EntityTypeBuilder<IssueStatus> issueStatus)
    {
        issueStatus.ToTable("IssueStatuses");
        issueStatus.HasKey(i => i.Id).IsClustered();
        issueStatus.Property(i => i.Name).HasMaxLength(64);
        issueStatus.HasData(
            new IssueStatus()
            {
                Id = (int)DomainEnums.IssueStatus.New,
                Name = "New",
                CreatedDate = this.dateTimeOffset,
                ModifiedDate = this.dateTimeOffset
            },
            new IssueStatus()
            {
                Id = (int)DomainEnums.IssueStatus.ForAnalysis,
                Name = "For Analysis",
                CreatedDate = this.dateTimeOffset,
                ModifiedDate = this.dateTimeOffset
            },
            new IssueStatus()
            {
                Id = (int)DomainEnums.IssueStatus.InProgress,
                Name = "In Progress",
                CreatedDate = this.dateTimeOffset,
                ModifiedDate = this.dateTimeOffset
            },
            new IssueStatus()
            {
                Id = (int)DomainEnums.IssueStatus.Resolved,
                Name = "Resolved",
                CreatedDate = this.dateTimeOffset,
                ModifiedDate = this.dateTimeOffset
            },
            new IssueStatus()
            {
                Id = (int)DomainEnums.IssueStatus.Closed,
                Name = "Closed",
                CreatedDate = this.dateTimeOffset,
                ModifiedDate = this.dateTimeOffset
            }
        );
    }
}
