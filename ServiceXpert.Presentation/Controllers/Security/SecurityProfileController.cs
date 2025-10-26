using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.Services.Contracts.Security;

namespace ServiceXpert.Presentation.Controllers.Security;
[Route("Security/Users/Profiles")]
[ApiController]
public class SecurityProfileController : SxpController
{
    private readonly ISecurityProfileService securityProfileService;

    public SecurityProfileController(ISecurityProfileService securityProfileService)
    {
        this.securityProfileService = securityProfileService;
    }

    [HttpGet("SearchProfileByName")]
    public async Task<IActionResult> SearchProfileByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return ApiResponse(await this.securityProfileService.SearchProfileByName(name, cancellationToken));
    }

    [HttpGet("{profileId:guid}")]
    public async Task<IActionResult> GetProfileByIdAsync(Guid profileId, CancellationToken cancellationToken = default)
    {
        var resultOnGet = await this.securityProfileService.GetByIdAsync(profileId, cancellationToken: cancellationToken);
        return ApiResponse(resultOnGet);
    }
}
