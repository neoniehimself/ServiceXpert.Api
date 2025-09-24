using Mapster;
using MapsterMapper;
using ServiceXpert.Application.DataObjects.AspNetUserProfile;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Application.Shared;
using ServiceXpert.Application.Shared.Enums;
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

    public override async Task<Result<Guid>> CreateAsync<TDataObjectForCreate>(TDataObjectForCreate dataObjectForCreate)
    {
        if (dataObjectForCreate is not AspNetUserProfileDataObjectForCreate)
        {
            return Result<Guid>.Fail(ResultStatus.ValidationError, "The data object must be of type " + nameof(AspNetUserProfileDataObjectForCreate));
        }

        var userProfile = this.mapper.Map<AspNetUserProfile>(dataObjectForCreate);

        await this.userProfileRepository.CreateAsync(userProfile);
        await this.userProfileRepository.SaveChangesAsync();

        return Result<Guid>.Ok(userProfile.Id);
    }

    public async Task<Result<IEnumerable<AspNetUserProfileDataObject>>> SearchUserByName(string searchQuery)
    {
        var userProfiles = await this.userProfileRepository.SearchUserByName(searchQuery);
        var userProfilesToReturn = userProfiles.Adapt<IEnumerable<AspNetUserProfileDataObject>>();

        return Result<IEnumerable<AspNetUserProfileDataObject>>.Ok(userProfilesToReturn);
    }
}
