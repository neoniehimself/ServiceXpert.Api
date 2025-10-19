using ServiceXpert.Domain.Entities.Issues;
using ServiceXpert.Domain.Repositories.Issues;
using ServiceXpert.Infrastructure.DbContexts;

namespace ServiceXpert.Infrastructure.Repositories.Issues;
internal class IssueCommentRepository : RepositoryBase<Guid, IssueComment>, IIssueCommentRepository
{
    public IssueCommentRepository(SxpDbContext dbContext) : base(dbContext)
    {
    }
}
