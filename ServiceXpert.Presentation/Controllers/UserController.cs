using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.Models.Auth;
using ServiceXpert.Application.Services.Contracts.Security;
using ServiceXpert.Domain.Enums.Security;

namespace ServiceXpert.Presentation.Controllers;
[Route("Users")]
[ApiController]
public class UserController : SxpController
{
    private readonly ISecurityUserService aspNetUserService;
    private readonly ISecurityProfileService aspNetUserProfileService;

    public UserController(ISecurityUserService aspNetUserService, ISecurityProfileService aspNetUserProfileService)
    {
        this.aspNetUserService = aspNetUserService;
        this.aspNetUserProfileService = aspNetUserProfileService;
    }

    [Authorize(Policy = nameof(SecurityPolicy.AdminOnly))]
    [HttpPost("AssignRoleToUser")]
    public async Task<IActionResult> AssignRoleToUserAsync(UserRole userRole)
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
