using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects.Issue;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Application.Shared.Utils;
using ServiceXpert.Presentation.Models.QueryOptions;
using System.Net;

namespace ServiceXpert.Presentation.Controllers;
[Authorize]
[Route("Api/Issues")]
[ApiController]
public class IssueController : SxpController
{
    private readonly IIssueService issueService;

    public IssueController(IIssueService issueService)
    {
        this.issueService = issueService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(IssueDataObjectForCreate issueForCreate)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequestInvalidModelState();
        }

        var resultOnCreate = await this.issueService.CreateAsync(issueForCreate);
        return ApiResponse(resultOnCreate);
    }

    [HttpGet("{issueKey}")]
    public async Task<IActionResult> GetByIssueKeyAsync(string issueKey)
    {
        var resultOnGet = await this.issueService.GetByIdAsync(IssueUtil.GetIdFromIssueKey(issueKey));
        return ApiResponse(resultOnGet);
    }

    [HttpGet]
    public async Task<IActionResult> GetPagedIssuesByStatusAsync([FromQuery] GetPagedIssuesByStatusQueryOption queryOption)
    {
        var resultOnGet = await this.issueService.GetPagedIssuesByStatusAsync(queryOption.StatusCategory, queryOption.PageNumber, queryOption.PageSize);
        return ApiResponse(resultOnGet);
    }

    [HttpPut("{issueKey}")]
    public async Task<IActionResult> UpdateAsync(string issueKey, IssueDataObjectForUpdate issueForUpdate)
    {
        var resultOnExists = await this.issueService.IsExistsByIdAsync(IssueUtil.GetIdFromIssueKey(issueKey));

        if (!resultOnExists.IsSuccess)
        {
            return NotFound(Models.ApiResponse.Fail(HttpStatusCode.NotFound, resultOnExists.Errors));
        }

        if (!this.ModelState.IsValid)
        {
            return BadRequestInvalidModelState();
        }

        var resultOnUpdate = await this.issueService.UpdateByIdAsync(IssueUtil.GetIdFromIssueKey(issueKey), issueForUpdate);
        return ApiResponse(resultOnUpdate);
    }
}
