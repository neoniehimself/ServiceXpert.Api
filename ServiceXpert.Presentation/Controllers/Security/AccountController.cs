using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.Models.Auth;
using ServiceXpert.Application.Services.Contracts.Security;
using ServiceXpert.Domain.Enums.Security;
using System.Security.Claims;

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

    [Authorize(Policy = nameof(SecurityPolicy.AdminOnly))]
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync(RegisterUser registerUser, CancellationToken cancellationToken = default)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequestInvalidModelState();
        }

        var result = await this.securityUserService.RegisterAsync(registerUser, cancellationToken);
        return ApiResponse(result);
    }

    [HttpPost("UpdatePasswordByUserName")]
    public async Task<IActionResult> UpdatePasswordByUserNameAsync(PasswordUpdate passwordUpdate, CancellationToken cancellationToken = default)
    {
        var userName = this.User.FindFirstValue(ClaimTypes.Name);
        var resultOnUpdate = await this.securityUserService.UpdatePasswordByUserNameAsync(userName, passwordUpdate, cancellationToken);
        return ApiResponse(resultOnUpdate);
    }
}
