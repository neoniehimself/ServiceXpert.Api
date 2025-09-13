using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects.Security;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Presentation.Controllers;
[Authorize(Policy = nameof(Policy.Admin))]
[Route("Api/Account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAspNetUserService aspNetUserService;
    private readonly IAspNetRoleService aspNetRoleService;

    public AccountController(
        IAspNetUserService aspNetUserService,
        IAspNetRoleService aspNetRoleService)
    {
        this.aspNetUserService = aspNetUserService;
        this.aspNetRoleService = aspNetRoleService;
    }

    [HttpPost(nameof(RegisterUserAsync))]
    public async Task<ActionResult> RegisterUserAsync(RegisterUserDataObject dataObject)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        var result = await this.aspNetUserService.RegisterAsync(dataObject);
        return result.Succeeded ? Ok("Register successful!") : BadRequest(result.Errors);
    }

    [AllowAnonymous]
    [HttpPost(nameof(LoginUserAsync))]
    public async Task<ActionResult> LoginUserAsync(LoginUserDataObject dataObject)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        var result = await this.aspNetUserService.LoginAsync(dataObject);
        return result.Succeeded ? Ok(new { result.token }) : BadRequest(result.Errors);
    }

    //[HttpPost(nameof(CreateRoleAsync))]
    //public async Task<ActionResult> CreateRoleAsync(string roleName)
    //{
    //    var result = await this.aspNetRoleService.CreateRoleAsync(roleName);
    //    return result.Succeeded ? Ok("Role created successfully!") : BadRequest(result.Errors);
    //}

    [HttpPost(nameof(AssignRoleToUserAsync))]
    public async Task<ActionResult> AssignRoleToUserAsync(UserRoleDataObject dataObject)
    {
        var result = await this.aspNetRoleService.AssignRoleAsync(dataObject);
        return result.Succeeded ? Ok("Role assigned successfully!") : BadRequest(result.Errors);
    }
}
