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
    [HttpPost(nameof(RegisterUserAsync))]
    public async Task<ActionResult> RegisterUserAsync(RegisterUserDataObject registerUser)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        var result = await this.aspNetUserService.RegisterAsync(registerUser);
        return result.Succeeded ? Ok("Register successful!") : BadRequest(result.Errors);
    }

    [AllowAnonymous]
    [HttpPost(nameof(LoginUserAsync))]
    public async Task<ActionResult> LoginUserAsync(LoginUserDataObject loginUser)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        var result = await this.aspNetUserService.LoginAsync(loginUser);
        return result.Succeeded ? Ok(result.token) : BadRequest(result.Errors);
    }
}
