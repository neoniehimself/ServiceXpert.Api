using MapsterMapper;
using ServiceXpert.Application.DataObjects.AspNetUserProfile;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Repositories;

namespace ServiceXpert.Application.Services;
public class AspNetUserProfileService : ServiceBase<Guid, AspNetUserProfile, AspNetUserProfileDataObject>, IAspNetUserProfileService
{
    private readonly IAspNetUserProfileRepository userProfileRepository;
    private readonly IMapper mapper;

    public AspNetUserProfileService(IAspNetUserProfileRepository userProfileRepository, IMapper mapper) : base(userProfileRepository, mapper)
    {
        this.userProfileRepository = userProfileRepository;
        this.mapper = mapper;
    }

    public override async Task<Guid> CreateAsync<TDataObjectForCreate>(TDataObjectForCreate dataObject)
    {
        if (dataObject is not AspNetUserProfileDataObjectForCreate)
        {
            throw new ArgumentException($"The data object must be of type {nameof(AspNetUserProfileDataObjectForCreate)}", nameof(dataObject));
        }

        var userProfile = this.mapper.Map<AspNetUserProfile>(dataObject);

        await this.userProfileRepository.CreateAsync(userProfile);
        await this.userProfileRepository.SaveChangesAsync();

        return userProfile.Id;
    }
}
