using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities;

namespace ServiceXpert.Infrastructure.DbContexts;
internal class IssueDbContext : DbContextBase, IEntityTypeConfiguration<Issue>
{
    public void Configure(EntityTypeBuilder<Issue> issue)
    {
        issue.HasKey(i => i.Id).IsClustered();
        issue.Property(i => i.Name).HasMaxLength(256);
        issue.Property(i => i.Description).HasMaxLength(4096);
        issue.HasOne(i => i.IssueStatus).WithOne().HasForeignKey<Issue>(i => i.IssueStatusId);
        issue.HasOne(i => i.IssuePriority).WithOne().HasForeignKey<Issue>(i => i.IssuePriorityId);
        issue.HasMany(i => i.Comments).WithOne().HasForeignKey(c => c.IssueId);
        issue.HasOne(i => i.Assignee).WithOne().HasForeignKey<Issue>(i => i.AssigneeId);
        issue.Navigation(i => i.IssueStatus).AutoInclude();
        issue.Navigation(i => i.IssuePriority).AutoInclude();
    }
}
