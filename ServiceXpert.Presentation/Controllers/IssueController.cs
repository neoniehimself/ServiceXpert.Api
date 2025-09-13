using FluentBuilder.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects.Issue;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Application.Shared.Utils;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Shared.Enums;
using ServiceXpert.Domain.Shared.ValueObjects;

namespace ServiceXpert.Presentation.Controllers;
[Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.User)}")]
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
        var propList = new PropertyList<Issue>();

        if (includeComments)
        {
            propList.Add(i => i.Comments);
        }

        var issue = await this.issueService.GetByIdAsync(IssueUtil.GetIdFromIssueKey(issueKey), propList.Count > 0 ? new IncludeOptions<Issue>(propList) : null);
        return issue != null ? Ok(issue) : NotFound($"{issueKey} not found.");
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<IssueDataObject>>> GetPagedIssuesByStatusAsync(
        string statusCategory = "All", int pageNumber = 1, int pageSize = 10, bool includeComments = false)
    {
        var propList = new PropertyList<Issue>();

        if (includeComments)
        {
            propList.Add(i => i.Comments);
        }

        var pagedResult = await this.issueService.GetPagedIssuesByStatusAsync(statusCategory, pageNumber, pageSize, propList.Count > 0 ? new IncludeOptions<Issue>(propList) : null);
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
