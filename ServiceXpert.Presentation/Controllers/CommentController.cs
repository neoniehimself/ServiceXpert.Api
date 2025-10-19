using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects.Issues;
using ServiceXpert.Application.Services.Contracts.Issues;
using ServiceXpert.Application.Utils;
using System.Net;

namespace ServiceXpert.Presentation.Controllers;
[Route("Issues/{issueKey}/Comments")]
[ApiController]
public class CommentController : SxpController
{
    private readonly IIssueCommentService commentService;
    private readonly IIssueService issueService;

    public CommentController(IIssueCommentService commentService, IIssueService issueService)
    {
        this.commentService = commentService;
        this.issueService = issueService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(string issueKey, CreateCommentDataObject commentForCreate)
    {
        if (!string.Equals(issueKey, commentForCreate.IssueKey))
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

        var resultOnCreate = await this.commentService.CreateAsync(commentForCreate);
        return ApiResponse(resultOnCreate);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllByIssueKeyAsync(string issueKey)
    {
        var issueId = IssueUtil.GetIdFromKey(issueKey);

        var resultOnExists = await this.issueService.IsExistsByIdAsync(issueId);

        if (!resultOnExists.IsSuccess)
        {
            return NotFound(Models.ApiResponse.Fail(HttpStatusCode.NotFound, resultOnExists.Errors));
        }

        var resultOnGet = await this.commentService.GetAllByIssueKeyAsync(issueId);
        return ApiResponse(resultOnGet);
    }
}
