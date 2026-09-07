using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects.Security;
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

    [HttpGet("GetMatchingProfilesByName")]
    public async Task<IActionResult> GetMatchingProfilesByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return ApiResponse(await this.securityProfileService.GetMatchingProfilesByNameAsync(name, cancellationToken));
    }

    [HttpGet("{profileId:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid profileId, CancellationToken cancellationToken = default)
    {
        var resultOnGet = await this.securityProfileService.GetByIdAsync(profileId, cancellationToken);
        return ApiResponse(resultOnGet);
    }

    [HttpPut("{profileId:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid profileId, UpdateSecurityProfileDataObject updateObj, CancellationToken cancellationToken = default)
    {
        var resultOnUpdate = await this.securityProfileService.UpdateByIdAsync(profileId, updateObj, cancellationToken);
        return ApiResponse(resultOnUpdate);
    }
}
