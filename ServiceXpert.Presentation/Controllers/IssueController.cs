using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects.Issue;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Application.Utils;
using ServiceXpert.Domain.Shared.Enums;
using ServiceXpert.Domain.ValueObjects;

namespace ServiceXpert.Presentation.Controllers;
[Route("api/issues")]
[ApiController]
public class IssueController : ControllerBase
{
    private readonly IIssueService issueService;

    public IssueController(IIssueService issueService)
    {
        this.issueService = issueService;
    }

    [HttpGet]
    public async Task<ActionResult<(IEnumerable<IssueDataObject>, Pagination)>> GetPagedAllByStatusAsync(
        string statusCategory = "All", int pageNumber = 1, int pageSize = 10)
    {
        var (issues, pagination) = await this.issueService.GetPagedAllByStatusAsync(statusCategory, pageNumber, pageSize);

        return Ok(new
        {
            issues,
            pagination
        });
    }

    [HttpGet("{issueKey}")]
    public async Task<ActionResult<IssueDataObject>> GetByIssueKeyAsync(string issueKey)
    {
        var issue = await this.issueService.GetByIssueKeyAsync(issueKey);
        return issue != null ? issue : NotFound($"{issueKey} not found.");
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateAsync(IssueDataObjectForCreate dataObject)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        var issueId = await this.issueService.CreateAsync(dataObject);
        return string.Concat(nameof(IssuePreFix.SXP), '-', issueId);
    }

    [HttpPut("{issueKey}")]
    public async Task<ActionResult> UpdateAsync(string issueKey, IssueDataObjectForUpdate dataObject)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        await this.issueService.UpdateByIdAsync(IssueUtil.GetIdFromIssueKey(issueKey), dataObject);
        return NoContent();
    }
}
