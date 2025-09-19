using FluentBuilder.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects.Issue;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Application.Shared.Utils;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Shared.Enums;
using ServiceXpert.Domain.Shared.ValueObjects;
using ServiceXpert.Presentation.Models.QueryOptions;

namespace ServiceXpert.Presentation.Controllers;
[Authorize]
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
    public async Task<ActionResult<string>> CreateAsync(IssueDataObjectForCreate issueForCreate)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        var issueId = await this.issueService.CreateAsync(issueForCreate);
        return Ok(string.Concat(nameof(IssuePreFix.SXP), '-', issueId));
    }

    [HttpGet("{issueKey}")]
    public async Task<ActionResult> GetByIssueKeyAsync(string issueKey)
    {
        var propList = new PropertyList<Issue>();

        var issue = await this.issueService.GetByIdAsync(IssueUtil.GetIdFromIssueKey(issueKey), propList.Count > 0 ? new IncludeOptions<Issue>(propList) : null);
        return issue != null ? Ok(issue) : NotFound($"{issueKey} not found.");
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<IssueDataObject>>> GetPagedIssuesByStatusAsync([FromQuery] GetPagedIssuesByStatusQueryOption queryOption)
    {
        var propList = new PropertyList<Issue>();

        if (queryOption.IncludeCreatedByUser) propList.Add(i => i.CreatedByUser!);
        if (queryOption.IncludeAssignee) propList.Add(i => i.Assignee);

        var pagedResult = await this.issueService.GetPagedIssuesByStatusAsync(queryOption.StatusCategory, queryOption.PageNumber, queryOption.PageSize, propList.Count > 0 ? new IncludeOptions<Issue>(propList) : null);
        return Ok(pagedResult);
    }

    [HttpPut("{issueKey}")]
    public async Task<ActionResult> UpdateAsync(string issueKey, IssueDataObjectForUpdate issueForUpdate)
    {
        if (!await this.issueService.IsExistsByIdAsync(IssueUtil.GetIdFromIssueKey(issueKey)))
        {
            return NotFound($"{issueKey} not found.");
        }

        if (!this.ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        await this.issueService.UpdateByIdAsync(IssueUtil.GetIdFromIssueKey(issueKey), issueForUpdate);
        return NoContent();
    }
}
