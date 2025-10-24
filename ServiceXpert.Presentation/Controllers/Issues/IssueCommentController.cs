using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects.Issues;
using ServiceXpert.Application.Services.Contracts.Issues;
using ServiceXpert.Application.Utils;
using System.Net;

namespace ServiceXpert.Presentation.Controllers.Issues;
[Route("Issues/{issueKey}/Comments")]
[ApiController]
public class IssueCommentController : SxpController
{
    private readonly IIssueService issueService;
    private readonly IIssueCommentService issueCommentService;

    public IssueCommentController(IIssueService issueService, IIssueCommentService issueCommentService)
    {
        this.issueService = issueService;
        this.issueCommentService = issueCommentService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(string issueKey, CreateIssueCommentDataObject createIssueComment)
    {
        if (!string.Equals(issueKey, createIssueComment.IssueKey))
        {
            return BadRequest(Models.ApiResponse.Fail(HttpStatusCode.BadRequest, ["URL's issue key and comment's issue key does not match"]));
        }

        var resultOnExists = await this.issueService.IsExistsByIdAsync(IssueUtil.GetIdFromKey(issueKey));

        if (!resultOnExists.IsSuccess)
        {
            return NotFound(Models.ApiResponse.Fail(HttpStatusCode.NotFound, resultOnExists.Errors));
        }

        if (!this.ModelState.IsValid)
        {
            return BadRequestInvalidModelState();
        }

        var resultOnCreate = await this.issueCommentService.CreateAsync(createIssueComment);
        return ApiResponse(resultOnCreate);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllByIssueKeyAsync(string issueKey)
    {
        var resultOnExists = await this.issueService.IsExistsByIdAsync(IssueUtil.GetIdFromKey(issueKey));

        if (!resultOnExists.IsSuccess)
        {
            return NotFound(Models.ApiResponse.Fail(HttpStatusCode.NotFound, resultOnExists.Errors));
        }

        var resultOnGet = await this.issueCommentService.GetAllByIssueKeyAsync(issueKey);
        return ApiResponse(resultOnGet);
    }
}
