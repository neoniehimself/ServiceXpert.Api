using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Application.Services.Contracts;

namespace ServiceXpert.Presentation.Controllers;
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

    [HttpPost(nameof(RegisterAsync))]
    public async Task<ActionResult> RegisterAsync(RegisterUserDataObject dataObject)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        var result = await this.aspNetUserService.RegisterAsync(dataObject);
        return result.Succeeded ? Ok("Register successful!") : BadRequest(result.Errors);
    }

    [HttpPost(nameof(LoginAsync))]
    public async Task<ActionResult> LoginAsync(LoginUserDataObject dataObject)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        var result = await this.aspNetUserService.LoginAsync(dataObject);
        return result.Succeeded ? Ok(result.token) : BadRequest(result.Errors);
    }

    [HttpPost(nameof(CreateAsync))]
    public async Task<ActionResult> CreateAsync(string roleName)
    {
        var result = await this.aspNetRoleService.CreateRoleAsync(roleName);
        return result.Succeeded ? Ok("Role created successfully!") : BadRequest(result.Errors);
    }

    [HttpPost(nameof(AssignRoleAsync))]
    public async Task<ActionResult> AssignRoleAsync(UserRoleDataObject dataObject)
    {
        var result = await this.aspNetRoleService.AssignRoleAsync(dataObject);
        return result.Succeeded ? Ok("Role assigned successfully!") : BadRequest(result.Errors);
    }
}
