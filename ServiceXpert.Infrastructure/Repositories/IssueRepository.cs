using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Repositories;
using ServiceXpert.Infrastructure.DbContexts;

namespace ServiceXpert.Infrastructure.Repositories;
public class IssueRepository : RepositoryBase<int, Issue>, IIssueRepository
{
    public IssueRepository(SxpDbContext dbContext) : base(dbContext)
    {
    }
}
