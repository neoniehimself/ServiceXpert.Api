using Microsoft.EntityFrameworkCore;
using ServiceXpert.Domain.Entities.Security;
using ServiceXpert.Domain.Repositories.Security;
using ServiceXpert.Infrastructure.DbContexts;

namespace ServiceXpert.Infrastructure.Repositories;
public class AspNetUserProfileRepository : RepositoryBase<Guid, SecurityProfile>, ISecurityProfileRepository
{
    private readonly SxpDbContext dbContext;

    public AspNetUserProfileRepository(SxpDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<SecurityProfile>> SearchUserByName(string searchQuery)
    {
        searchQuery = searchQuery.Trim().Replace(' ', '%');
        searchQuery = $"%{searchQuery}%";

        return await this.dbContext.AspNetUserProfiles
            .FromSqlInterpolated(@$"
SELECT TOP 5 * FROM [AspNetUserProfiles]
WHERE CONCAT([FirstName], ' ', [LastName]) LIKE {searchQuery}")
            .TagWith($"{nameof(AspNetUserProfileRepository)}.{nameof(SearchUserByName)}")
            .ToListAsync();
    }
}
