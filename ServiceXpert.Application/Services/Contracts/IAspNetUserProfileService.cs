using ServiceXpert.Application.DataObjects.AspNetUserProfile;
using ServiceXpert.Application.Shared;
using ServiceXpert.Domain.Entities;

namespace ServiceXpert.Application.Services.Contracts;
public interface IAspNetUserProfileService : IServiceBase<Guid, AspNetUserProfile, AspNetUserProfileDataObject>
{
    Task<Result<IEnumerable<AspNetUserProfileDataObject>>> SearchUserByName(string searchQuery);
}
