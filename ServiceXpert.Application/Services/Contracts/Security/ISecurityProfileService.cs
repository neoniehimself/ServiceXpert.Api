using ServiceXpert.Application.DataObjects.Security;
using ServiceXpert.Application.Models;
using ServiceXpert.Domain.Entities.Security;

namespace ServiceXpert.Application.Services.Contracts.Security;
public interface ISecurityProfileService : IServiceBase<Guid, SecurityProfile, SecurityProfileDataObject>
{
    Task<ServiceResult<IEnumerable<SecurityProfileDataObject>>> SearchProfileByName(string name);
}
