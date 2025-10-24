using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects.Issues;
using ServiceXpert.Application.Services.Contracts.Issues;
using ServiceXpert.Application.Utils;
using ServiceXpert.Presentation.Models.QueryOptions;
using System.Net;

namespace ServiceXpert.Presentation.Controllers.Issues;
[Route("Issues")]
[ApiController]
public class IssueController : SxpController
{
    private readonly IIssueService issueService;

    public IssueController(IIssueService issueService)
    {
        this.issueService = issueService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateIssueDataObject createIssue)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequestInvalidModelState();
        }

        var resultOnCreate = await this.issueService.CreateAsync(createIssue);
        return ApiResponse(resultOnCreate);
    }

    [HttpGet("{issueKey}")]
    public async Task<IActionResult> GetByIssueKeyAsync(string issueKey)
    {
        var resultOnGet = await this.issueService.GetByIdAsync(IssueUtil.GetIdFromKey(issueKey));
        return ApiResponse(resultOnGet);
    }

    [HttpGet]
    public async Task<IActionResult> GetPagedIssuesByStatusAsync([FromQuery] GetPagedIssuesByStatusQueryOption queryOption)
    {
        var resultOnGet = await this.issueService.GetPagedIssuesByStatusAsync(queryOption.StatusCategory, queryOption.PageNumber, queryOption.PageSize);
        return ApiResponse(resultOnGet);
    }

    [HttpPut("{issueKey}")]
    public async Task<IActionResult> UpdateAsync(string issueKey, UpdateIssueDataObject updateIssue)
    {
        var resultOnExists = await this.issueService.IsExistsByIdAsync(IssueUtil.GetIdFromKey(issueKey));

        if (!resultOnExists.IsSuccess)
        {
            return NotFound(Models.ApiResponse.Fail(HttpStatusCode.NotFound, resultOnExists.Errors));
        }

        if (!this.ModelState.IsValid)
        {
            return BadRequestInvalidModelState();
        }

        var resultOnUpdate = await this.issueService.UpdateByIdAsync(IssueUtil.GetIdFromKey(issueKey), updateIssue);
        return ApiResponse(resultOnUpdate);
    }
}
