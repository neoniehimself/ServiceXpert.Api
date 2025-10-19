using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.Services.Contracts.Security;
using ServiceXpert.Domain.Enums.Security;

namespace ServiceXpert.Presentation.Controllers;
[Authorize(Policy = nameof(SecurityPolicy.AdminOnly))]
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
