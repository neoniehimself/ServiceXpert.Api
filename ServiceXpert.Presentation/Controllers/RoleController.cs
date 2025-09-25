using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Presentation.Controllers;
[Authorize(Policy = nameof(Policy.AdminOnly))]
[Route("Roles")]
[ApiController]
public class RoleController : SxpController
{
    private readonly IAspNetRoleService aspNetRoleService;

    public RoleController(IAspNetRoleService aspNetRoleService)
    {
        this.aspNetRoleService = aspNetRoleService;
    }

    [HttpPost(nameof(CreateRoleAsync))]
    public async Task<IActionResult> CreateRoleAsync(string roleName)
    {
        var resultOnCreate = await this.aspNetRoleService.CreateRoleAsync(roleName);
        return ApiResponse(resultOnCreate);
    }
}
