using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities;
using DomainEnums = ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Infrastructure.DbContexts;
internal class IssueStatusDbContext : DbContextBase, IEntityTypeConfiguration<IssueStatus>
{
    private readonly DateTime dateTime = new(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc);

    public void Configure(EntityTypeBuilder<IssueStatus> issueStatus)
    {
        issueStatus.ToTable("IssueStatuses");
        issueStatus.HasKey(i => i.IssueStatusId).IsClustered();
        issueStatus.Property(i => i.Name).HasMaxLength(64);
        issueStatus.HasData(
            new IssueStatus()
            {
                IssueStatusId = (int)DomainEnums.IssueStatus.New,
                Name = "New",
                CreatedDate = this.dateTime,
                ModifyDate = this.dateTime
            },
            new IssueStatus()
            {
                IssueStatusId = (int)DomainEnums.IssueStatus.ForAnalysis,
                Name = "For Analysis",
                CreatedDate = this.dateTime,
                ModifyDate = this.dateTime
            },
            new IssueStatus()
            {
                IssueStatusId = (int)DomainEnums.IssueStatus.InProgress,
                Name = "In Progress",
                CreatedDate = this.dateTime,
                ModifyDate = this.dateTime
            },
            new IssueStatus()
            {
                IssueStatusId = (int)DomainEnums.IssueStatus.Resolved,
                Name = "Resolved",
                CreatedDate = this.dateTime,
                ModifyDate = this.dateTime
            },
            new IssueStatus()
            {
                IssueStatusId = (int)DomainEnums.IssueStatus.Closed,
                Name = "Closed",
                CreatedDate = this.dateTime,
                ModifyDate = this.dateTime
            }
        );
    }
}
