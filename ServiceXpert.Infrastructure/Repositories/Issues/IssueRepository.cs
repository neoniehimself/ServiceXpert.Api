using ServiceXpert.Domain.Entities.Issues;
using ServiceXpert.Domain.Repositories.Issues;
using ServiceXpert.Infrastructure.DbContexts;

namespace ServiceXpert.Infrastructure.Repositories.Issues;
internal class IssueRepository : RepositoryBase<int, Issue>, IIssueRepository
{
    public IssueRepository(SxpDbContext dbContext) : base(dbContext)
    {
    }
}
