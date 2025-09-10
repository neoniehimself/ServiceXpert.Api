using Mapster;
using Microsoft.AspNetCore.Identity;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Application.DataObjects.AspNetUserProfile;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Infrastructure.AuthModels;

namespace ServiceXpert.Infrastructure.Services;
public class AspNetUserService : IAspNetUserService
{
    private readonly UserManager<AspNetUser> userManager;
    private readonly IAspNetUserProfileService aspNetUserProfileService;

    public AspNetUserService(UserManager<AspNetUser> userManager, IAspNetUserProfileService aspNetUserProfileService)
    {
        this.userManager = userManager;
        this.aspNetUserProfileService = aspNetUserProfileService;
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors, Guid aspNetUserId)> RegisterAsync(RegisterUserDataObject dataObject)
    {
        var result = await this.userManager.CreateAsync(new AspNetUser()
        {
            UserName = dataObject.UserName,
            Email = dataObject.Email
        }, dataObject.Password);

        if (!result.Succeeded)
        {
            return (false, result.Errors.Select(e => e.Description), Guid.Empty);
        }

        var aspNetUser = await this.userManager.FindByNameAsync(dataObject.UserName);

        var config = new TypeAdapterConfig();
        config.ForType<RegisterUserDataObject, AspNetUserProfileDataObjectForCreate>()
              .MapToConstructor(true)
              .ConstructUsing(src => new AspNetUserProfileDataObjectForCreate(aspNetUser!.Id));

        var aspNetUserProfile = dataObject.Adapt<AspNetUserProfileDataObjectForCreate>(config);
        await this.aspNetUserProfileService.CreateAsync(aspNetUserProfile);

        return (true, Enumerable.Empty<string>(), aspNetUser!.Id);
    }
}
