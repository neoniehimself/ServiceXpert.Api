using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.Models.Auth;
using ServiceXpert.Application.Services.Contracts.Security;
using ServiceXpert.Domain.Enums.Security;

namespace ServiceXpert.Presentation.Controllers.Security;
[Route("Security/Accounts")]
[ApiController]
public class AccountController : SxpController
{
    private readonly ISecurityUserService securityUserService;

    public AccountController(ISecurityUserService securityUserService)
    {
        this.securityUserService = securityUserService;
    }

    [Authorize(Policy = nameof(SecurityPolicy.AdminOnly))]
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync(RegisterUser registerUser)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequestInvalidModelState();
        }

        var result = await this.securityUserService.RegisterAsync(registerUser);
        return ApiResponse(result);
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync(LoginUser loginUser)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequestInvalidModelState();
        }

        var result = await this.securityUserService.LoginAsync(loginUser);
        return ApiResponse(result);
    }
}
