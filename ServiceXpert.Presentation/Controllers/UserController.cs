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
    private readonly ISecurityUserService securityUserService;
    private readonly ISecurityProfileService securityProfileService;

    public UserController(ISecurityUserService securityUserService, ISecurityProfileService securityProfileService)
    {
        this.securityUserService = securityUserService;
        this.securityProfileService = securityProfileService;
    }

    [Authorize(Policy = nameof(SecurityPolicy.AdminOnly))]
    [HttpPost("AssignRoleToUser")]
    public async Task<IActionResult> AssignRoleToUserAsync(UserRole userRole)
    {
        var resultOnAssign = await this.securityUserService.AssignRoleAsync(userRole);
        return ApiResponse(resultOnAssign);
    }

    [HttpGet("SearchUserByName")]
    public async Task<IActionResult> SearchUserByNameAsync(string name)
    {
        return ApiResponse(await this.securityProfileService.SearchUserByName(name));
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserProfileByIdAsync(Guid userId)
    {
        var resultOnGet = await this.securityProfileService.GetByIdAsync(userId);
        return ApiResponse(resultOnGet);
    }
}
