using MapsterMapper;
using ServiceXpert.Application.DataObjects.AspNetUserProfile;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Repositories.Contracts;

namespace ServiceXpert.Application.Services;
public class AspNetUserProfileService : ServiceBase<Guid, AspNetUserProfile, AspNetUserProfileDataObject>, IAspNetUserProfileService
{
    public AspNetUserProfileService(IAspNetUserProfileRepository userProfileRepository, IMapper mapper) : base(userProfileRepository, mapper)
    {
    }
}
