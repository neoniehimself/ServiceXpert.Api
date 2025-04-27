using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Repositories.Contracts;
using ServiceXpert.ServiceXpert.Infrastructure.DbContexts;

namespace ServiceXpert.Infrastructure.Repositories;
public class IssueRepository : RepositoryBase<int, Issue>, IIssueRepository
{
    public IssueRepository(SxpDbContext dbContext) : base(dbContext)
    {
    }
}
