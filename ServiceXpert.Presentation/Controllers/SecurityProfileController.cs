using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.Services.Contracts.Security;

namespace ServiceXpert.Presentation.Controllers;
[Route("SecurityProfiles")]
[ApiController]
public class SecurityProfileController : SxpController
{
    private readonly ISecurityProfileService securityProfileService;

    public SecurityProfileController(ISecurityProfileService securityProfileService)
    {
        this.securityProfileService = securityProfileService;
    }

    [HttpGet("SearchProfileByName")]
    public async Task<IActionResult> SearchProfileByNameAsync(string name)
    {
        return ApiResponse(await this.securityProfileService.SearchProfileByName(name));
    }

    [HttpGet("{profileId:guid}")]
    public async Task<IActionResult> GetProfileByIdAsync(Guid profileId)
    {
        var resultOnGet = await this.securityProfileService.GetByIdAsync(profileId);
        return ApiResponse(resultOnGet);
    }
}
