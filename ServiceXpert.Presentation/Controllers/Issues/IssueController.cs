using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects.Issues;
using ServiceXpert.Application.Models.Issues.QueryOptions;
using ServiceXpert.Application.Services.Contracts.Issues;
using ServiceXpert.Application.Utils;
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
    public async Task<IActionResult> CreateAsync(CreateIssueDataObject createIssue, CancellationToken cancellationToken = default)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequestInvalidModelState();
        }

        var resultOnCreate = await this.issueService.CreateAsync(createIssue, cancellationToken);
        return ApiResponse(resultOnCreate);
    }

    [HttpGet("{issueKey}")]
    public async Task<IActionResult> GetByIssueKeyAsync(string issueKey, CancellationToken cancellationToken = default)
    {
        var resultOnGet = await this.issueService.GetByIdAsync(IssueUtil.GetIdFromKey(issueKey), cancellationToken: cancellationToken);
        return ApiResponse(resultOnGet);
    }

    [HttpGet]
    public async Task<IActionResult> GetPagedIssuesAsync([FromQuery] GetPagedIssuesQueryOption queryOption, CancellationToken cancellationToken = default)
    {
        var resultOnGet = await this.issueService.GetPagedIssuesAsync(queryOption, cancellationToken: cancellationToken);
        return ApiResponse(resultOnGet);
    }

    [HttpPut("{issueKey}")]
    public async Task<IActionResult> UpdateAsync(string issueKey, UpdateIssueDataObject updateIssue, CancellationToken cancellationToken = default)
    {
        var resultOnExists = await this.issueService.IsExistsByIdAsync(IssueUtil.GetIdFromKey(issueKey), cancellationToken);

        if (!resultOnExists.IsSuccess)
        {
            return NotFound(Models.ApiResponse.Fail(HttpStatusCode.NotFound, resultOnExists.Errors));
        }

        if (!this.ModelState.IsValid)
        {
            return BadRequestInvalidModelState();
        }

        var resultOnUpdate = await this.issueService.UpdateByIdAsync(IssueUtil.GetIdFromKey(issueKey), updateIssue, cancellationToken);
        return ApiResponse(resultOnUpdate);
    }
}
