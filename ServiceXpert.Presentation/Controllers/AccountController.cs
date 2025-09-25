using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects.Security;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Presentation.Controllers;
[Route("Api/Accounts")]
[ApiController]
public class AccountController : SxpController
{
    private readonly IAspNetUserService aspNetUserService;

    public AccountController(IAspNetUserService aspNetUserService)
    {
        this.aspNetUserService = aspNetUserService;
    }

    [Authorize(Policy = nameof(Policy.AdminOnly))]
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync(RegisterDataObject register)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequestInvalidModelState();
        }

        var result = await this.aspNetUserService.RegisterAsync(register);
        return ApiResponse(result);
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync(LoginDataObject login)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequestInvalidModelState();
        }

        var result = await this.aspNetUserService.LoginAsync(login);
        return ApiResponse(result);
    }
}
