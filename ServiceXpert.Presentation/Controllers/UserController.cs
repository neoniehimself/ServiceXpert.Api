using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects.Security;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Domain.Enums.Security;

namespace ServiceXpert.Presentation.Controllers;
[Route("Users")]
[ApiController]
public class UserController : SxpController
{
    private readonly IAspNetUserService aspNetUserService;
    private readonly IAspNetUserProfileService aspNetUserProfileService;

    public UserController(IAspNetUserService aspNetUserService, IAspNetUserProfileService aspNetUserProfileService)
    {
        this.aspNetUserService = aspNetUserService;
        this.aspNetUserProfileService = aspNetUserProfileService;
    }

    [Authorize(Policy = nameof(SecurityPolicy.AdminOnly))]
    [HttpPost("AssignRoleToUser")]
    public async Task<IActionResult> AssignRoleToUserAsync(UserRoleDataObject userRole)
    {
        var resultOnAssign = await this.aspNetUserService.AssignRoleAsync(userRole);
        return ApiResponse(resultOnAssign);
    }

    [HttpGet("SearchUserByName")]
    public async Task<IActionResult> SearchUserByNameAsync(string searchQuery)
    {
        return ApiResponse(await this.aspNetUserProfileService.SearchUserByName(searchQuery));
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserProfileByIdAsync(Guid userId)
    {
        var resultOnGet = await this.aspNetUserProfileService.GetByIdAsync(userId);
        return ApiResponse(resultOnGet);
    }
}
