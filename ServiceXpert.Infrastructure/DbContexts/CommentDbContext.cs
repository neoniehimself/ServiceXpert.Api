using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities.Issues;

namespace ServiceXpert.Infrastructure.DbContexts;
internal class CommentDbContext : DbContextBase, IEntityTypeConfiguration<IssueComment>
{
    public void Configure(EntityTypeBuilder<IssueComment> comment)
    {
        comment.HasKey(c => c.Id).IsClustered(false);
        comment.Property(c => c.Content);
        comment.HasOne<Issue>().WithMany(i => i.Comments).HasForeignKey(c => c.IssueId);
        comment.HasOne(c => c.CreatedByUser).WithMany().HasForeignKey(c => c.CreatedByUserId);
    }
}
