using ServiceXpert.Domain.Entities.Security;

namespace ServiceXpert.Domain.Repositories.Security;
public interface ISecurityProfileRepository : IRepositoryBase<Guid, SecurityProfile>
{
    Task<IEnumerable<SecurityProfile>> SearchUserByName(string name);
}
