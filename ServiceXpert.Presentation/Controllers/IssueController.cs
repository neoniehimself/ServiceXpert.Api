using FluentBuilder.Core;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects.Issue;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Application.Utils;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Shared.Enums;
using ServiceXpert.Domain.ValueObjects;

namespace ServiceXpert.Presentation.Controllers;
[Route("Api/Issues")]
[ApiController]
public class IssueController : ControllerBase
{
    private readonly IIssueService issueService;

    public IssueController(IIssueService issueService)
    {
        this.issueService = issueService;
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateAsync(IssueDataObjectForCreate dataObject)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        var issueId = await this.issueService.CreateAsync(dataObject);
        return Ok(string.Concat(nameof(IssuePreFix.SXP), '-', issueId));
    }

    [HttpGet("{issueKey}")]
    public async Task<ActionResult> GetByIssueKeyAsync(string issueKey, bool includeComments = false)
    {
        var issue = await this.issueService.GetByIdAsync(
            IssueUtil.GetIdFromIssueKey(issueKey),
            new IncludeOptions<Issue>(i => i.Comments));

        return issue != null ? Ok(issue) : NotFound($"{issueKey} not found.");
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<IssueDataObject>>> GetPagedIssuesByStatusAsync(
        string statusCategory = "All", int pageNumber = 1, int pageSize = 10)
    {
        var pagedResult = await this.issueService.GetPagedIssuesByStatusAsync(statusCategory, pageNumber, pageSize);
        pagedResult.Pagination.CurrentPage = pagedResult.Pagination.TotalCount > 0 ? pagedResult.Pagination.CurrentPage : 0;
        pagedResult.Pagination.PageSize = pagedResult.Pagination.TotalCount > 0 ? pagedResult.Pagination.PageSize : 0;
        return Ok(pagedResult);
    }

    [HttpPut("{issueKey}")]
    public async Task<ActionResult> UpdateAsync(string issueKey, IssueDataObjectForUpdate dataObject)
    {
        if (!await this.issueService.IsExistsByIdAsync(IssueUtil.GetIdFromIssueKey(issueKey)))
        {
            return NotFound($"{issueKey} not found.");
        }

        if (!this.ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        await this.issueService.UpdateByIdAsync(IssueUtil.GetIdFromIssueKey(issueKey), dataObject);
        return NoContent();
    }
}
