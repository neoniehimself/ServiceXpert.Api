using ServiceXpert.Application.DataObjects.AspNetUserProfile;
using ServiceXpert.Domain.Entities;

namespace ServiceXpert.Application.Services.Contracts;
public interface IAspNetUserProfileService : IServiceBase<Guid, AspNetUserProfile, AspNetUserProfileDataObject>
{
    Task<IEnumerable<AspNetUserProfileDataObject>> SearchUserByName(string searchQuery);
}
