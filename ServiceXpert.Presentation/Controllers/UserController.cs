using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects.AspNetUserProfile;
using ServiceXpert.Application.DataObjects.Security;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Presentation.Controllers;
[Route("Api/Users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IAspNetUserService aspNetUserService;
    private readonly IAspNetUserProfileService aspNetUserProfileService;

    public UserController(IAspNetUserService aspNetUserService, IAspNetUserProfileService aspNetUserProfileService)
    {
        this.aspNetUserService = aspNetUserService;
        this.aspNetUserProfileService = aspNetUserProfileService;
    }

    [Authorize(Policy = nameof(Policy.AdminOnly))]
    [HttpPost(nameof(AssignRoleToUserAsync))]
    public async Task<ActionResult> AssignRoleToUserAsync(UserRoleDataObject dataObject)
    {
        var result = await this.aspNetUserService.AssignRoleAsync(dataObject);
        return result.Succeeded ? Ok("Role assigned successfully!") : BadRequest(result.Errors);
    }

    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<AspNetUserProfileDataObject>> GetUserProfileByIdAsync(Guid userId)
    {
        var userProfile = await this.aspNetUserProfileService.GetByIdAsync(userId);
        return userProfile != null ? Ok(userProfile) : NotFound($"User profile not found. Id = {userId}.");
    }
}
