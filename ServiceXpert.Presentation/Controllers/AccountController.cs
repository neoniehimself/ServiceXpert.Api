using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.Models.Auth;
using ServiceXpert.Application.Services.Contracts.Security;
using ServiceXpert.Domain.Enums.Security;

namespace ServiceXpert.Presentation.Controllers;
[Route("Accounts")]
[ApiController]
public class AccountController : SxpController
{
    private readonly ISecurityUserService aspNetUserService;

    public AccountController(ISecurityUserService aspNetUserService)
    {
        this.aspNetUserService = aspNetUserService;
    }

    [Authorize(Policy = nameof(SecurityPolicy.AdminOnly))]
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync(RegisterUser register)
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
    public async Task<IActionResult> LoginAsync(LoginUser login)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequestInvalidModelState();
        }

        var result = await this.aspNetUserService.LoginAsync(login);
        return ApiResponse(result);
    }
}
