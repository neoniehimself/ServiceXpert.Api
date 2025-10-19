using Microsoft.EntityFrameworkCore;
using ServiceXpert.Domain.Entities.Security;
using ServiceXpert.Domain.Repositories.Security;
using ServiceXpert.Infrastructure.DbContexts;

namespace ServiceXpert.Infrastructure.Repositories.Security;
internal class SecurityProfileRepository : RepositoryBase<Guid, SecurityProfile>, ISecurityProfileRepository
{
    private readonly SxpDbContext dbContext;

    public SecurityProfileRepository(SxpDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<SecurityProfile>> SearchUserByName(string name)
    {
        name = name.Trim().Replace(' ', '%');
        name = $"%{name}%";

        return await this.dbContext.Set<SecurityProfile>()
            .FromSqlInterpolated(@$"SELECT TOP 5 * FROM [AspNetUserProfiles]
                                    WHERE CONCAT([FirstName], ' ', [LastName]) LIKE {name}")
            .TagWith($"{nameof(SecurityProfileRepository)}.{nameof(SearchUserByName)}")
            .ToListAsync();
    }
}
