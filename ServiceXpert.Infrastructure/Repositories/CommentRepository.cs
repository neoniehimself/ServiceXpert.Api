using ServiceXpert.Domain.Entities.Issues;
using ServiceXpert.Domain.Repositories.Issues;
using ServiceXpert.Infrastructure.DbContexts;

namespace ServiceXpert.Infrastructure.Repositories;
public class CommentRepository : RepositoryBase<Guid, IssueComment>, IIssueCommentRepository
{
    public CommentRepository(SxpDbContext dbContext) : base(dbContext)
    {
    }
}
