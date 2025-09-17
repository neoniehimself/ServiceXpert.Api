using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects.Security;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Presentation.Controllers;
[Route("Api/Users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IAspNetRoleService aspNetRoleService;

    public UserController(IAspNetRoleService aspNetRoleService)
    {
        this.aspNetRoleService = aspNetRoleService;
    }

    [Authorize(Policy = nameof(Policy.AdminOnly))]
    [HttpPost(nameof(AssignRoleToUserAsync))]
    public async Task<ActionResult> AssignRoleToUserAsync(UserRoleDataObject dataObject)
    {
        var result = await this.aspNetRoleService.AssignRoleAsync(dataObject);
        return result.Succeeded ? Ok("Role assigned successfully!") : BadRequest(result.Errors);
    }
}
