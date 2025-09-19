using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects.Security;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Presentation.Controllers;
[Route("Api/Accounts")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAspNetUserService aspNetUserService;

    public AccountController(IAspNetUserService aspNetUserService)
    {
        this.aspNetUserService = aspNetUserService;
    }

    [Authorize(Policy = nameof(Policy.AdminOnly))]
    [HttpPost("Register")]
    public async Task<ActionResult> RegisterAsync(RegisterDataObject register)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        var result = await this.aspNetUserService.RegisterAsync(register);
        return result.Succeeded ? Ok("Register successful!") : BadRequest(result.Errors);
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ActionResult> LoginAsync(LoginDataObject login)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        var result = await this.aspNetUserService.LoginAsync(login);
        return result.Succeeded ? Ok(result.token) : BadRequest(result.Errors);
    }
}
