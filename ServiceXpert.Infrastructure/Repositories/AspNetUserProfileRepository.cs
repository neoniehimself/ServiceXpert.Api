using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Repositories.Contracts;
using ServiceXpert.Infrastructure.DbContexts;

namespace ServiceXpert.Infrastructure.Repositories;
public class AspNetUserProfileRepository : RepositoryBase<Guid, AspNetUserProfile>, IAspNetUserProfileRepository
{
    public AspNetUserProfileRepository(SxpDbContext dbContext) : base(dbContext)
    {
    }
}
