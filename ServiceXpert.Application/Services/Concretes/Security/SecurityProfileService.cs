using Mapster;
using MapsterMapper;
using ServiceXpert.Application.DataObjects.Security;
using ServiceXpert.Application.Enums;
using ServiceXpert.Application.Models;
using ServiceXpert.Application.Services.Contracts.Security;
using ServiceXpert.Domain.Entities.Security;
using ServiceXpert.Domain.Repositories.Security;

namespace ServiceXpert.Application.Services.Concretes.Security;
internal class SecurityProfileService : ServiceBase<Guid, SecurityProfile, SecurityProfileDataObject>, ISecurityProfileService
{
    private readonly IMapper mapper;
    private readonly ISecurityProfileRepository userProfileRepository;

    public SecurityProfileService(IMapper mapper, ISecurityProfileRepository userProfileRepository) : base(mapper, userProfileRepository)
    {
        this.mapper = mapper;
        this.userProfileRepository = userProfileRepository;
    }

    public override async Task<ServiceResult<Guid>> CreateAsync<TCreateDataObject>(TCreateDataObject createDataObject)
    {
        if (createDataObject is not CreateSecurityProfileDataObject)
        {
            return ServiceResult<Guid>.Fail(ServiceResultStatus.ValidationError, ["The data object must be of type " + nameof(CreateSecurityProfileDataObject)]);
        }

        var userProfile = this.mapper.Map<SecurityProfile>(createDataObject);

        await this.userProfileRepository.CreateAsync(userProfile);
        await this.userProfileRepository.SaveChangesAsync();

        return ServiceResult<Guid>.Ok(userProfile.Id);
    }

    public async Task<ServiceResult<IEnumerable<SecurityProfileDataObject>>> SearchProfileByName(string name)
    {
        var profiles = await this.userProfileRepository.SearchProfileByName(name);
        var profilesToReturn = profiles.Adapt<IEnumerable<SecurityProfileDataObject>>();

        return ServiceResult<IEnumerable<SecurityProfileDataObject>>.Ok(profilesToReturn);
    }
}
