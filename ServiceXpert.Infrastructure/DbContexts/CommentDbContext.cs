using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceXpert.Domain.Entities;

namespace ServiceXpert.Infrastructure.DbContexts;
internal class CommentDbContext : DbContextBase, IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> comment)
    {
        comment.HasKey(c => c.Id).IsClustered(false);
        comment.Property(c => c.Content);
        comment.HasOne<Issue>().WithMany(i => i.Comments).HasForeignKey(c => c.IssueId);
    }
}
