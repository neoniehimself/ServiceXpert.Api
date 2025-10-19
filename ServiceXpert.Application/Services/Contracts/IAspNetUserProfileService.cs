using ServiceXpert.Application.DataObjects.AspNetUserProfile;
using ServiceXpert.Application.Shared;
using ServiceXpert.Domain.Entities.Security;

namespace ServiceXpert.Application.Services.Contracts;
public interface IAspNetUserProfileService : IServiceBase<Guid, SecurityProfile, AspNetUserProfileDataObject>
{
    Task<Result<IEnumerable<AspNetUserProfileDataObject>>> SearchUserByName(string searchQuery);
}
