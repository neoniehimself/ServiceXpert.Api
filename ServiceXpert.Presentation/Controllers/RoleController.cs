using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Presentation.Controllers;
[Route("Roles")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IAspNetRoleService aspNetRoleService;

    public RoleController(IAspNetRoleService aspNetRoleService)
    {
        this.aspNetRoleService = aspNetRoleService;
    }

    [Authorize(Policy = nameof(Policy.AdminOnly))]
    [HttpPost(nameof(CreateRoleAsync))]
    public async Task<ActionResult> CreateRoleAsync(string roleName)
    {
        var result = await this.aspNetRoleService.CreateRoleAsync(roleName);
        return result.Succeeded ? Ok("Role created successfully!") : BadRequest(result.Errors);
    }
}
