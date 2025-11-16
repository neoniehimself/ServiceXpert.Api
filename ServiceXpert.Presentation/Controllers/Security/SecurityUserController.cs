using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.Models.Auth;
using ServiceXpert.Application.Models.Security.QueryOptions;
using ServiceXpert.Application.Services.Contracts.Security;
using ServiceXpert.Domain.Enums.Security;

namespace ServiceXpert.Presentation.Controllers.Security;
[Route("Security/Users")]
[ApiController]
public class SecurityUserController : SxpController
{
    private readonly ISecurityUserService securityUserService;

    public SecurityUserController(ISecurityUserService securityUserService)
    {
        this.securityUserService = securityUserService;
    }

    [Authorize(Policy = nameof(SecurityPolicy.AdminOnly))]
    [HttpPost("AssignRoleToUser")]
    public async Task<IActionResult> AssignRoleToSecurityUserAsync(UserRole userRole)
    {
        var resultOnAssign = await this.securityUserService.AssignRoleAsync(userRole);
        return ApiResponse(resultOnAssign);
    }

    [HttpGet]
    public Task<IActionResult> GetPagedUsersAsync([FromQuery] GetPagedUsersQueryOption queryOption, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
