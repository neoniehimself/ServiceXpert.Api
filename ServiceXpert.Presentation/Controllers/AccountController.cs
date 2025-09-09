using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Application.DataObjects.AspNetUserProfile;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Infrastructure.Models;

namespace ServiceXpert.Presentation.Controllers;
[Route("Api/Account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<AspNetUser> userManager;
    private readonly RoleManager<AspNetRole> roleManager;
    private readonly IMapper mapper;
    private readonly IAspNetUserProfileService userProfileService;

    public AccountController(
        UserManager<AspNetUser> userManager,
        RoleManager<AspNetRole> roleManager,
        IMapper mapper,
        IAspNetUserProfileService userProfileService)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.mapper = mapper;
        this.userProfileService = userProfileService;
    }

    [HttpPost(nameof(Register))]
    public async Task<ActionResult> Register(RegisterUserDataObject dataObject)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        var result = await this.userManager.CreateAsync(new AspNetUser()
        {
            UserName = dataObject.UserName,
            Email = dataObject.Email
        }, dataObject.Password);

        if (result.Succeeded)
        {
            var aspNetUser = await this.userManager.FindByNameAsync(dataObject.UserName);

            var config = new TypeAdapterConfig();
            config.ForType<RegisterUserDataObject, AspNetUserProfileDataObjectForCreate>()
                  .MapToConstructor(true)
                  .ConstructUsing(src => new AspNetUserProfileDataObjectForCreate(aspNetUser!.Id));

            var aspNetUserProfile = dataObject.Adapt<AspNetUserProfileDataObjectForCreate>(config);

            await this.userProfileService.CreateAsync(aspNetUserProfile);
            return Ok("Regiter successful!");
        }

        return BadRequest(result.Errors);
    }
}
