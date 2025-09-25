using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects.Comment;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Application.Shared.Utils;
using System.Net;

namespace ServiceXpert.Presentation.Controllers;
[Route("Api/Issues/{issueKey}/Comments")]
[ApiController]
public class CommentController : SxpController
{
    private readonly ICommentService commentService;
    private readonly IIssueService issueService;

    public CommentController(ICommentService commentService, IIssueService issueService)
    {
        this.commentService = commentService;
        this.issueService = issueService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(string issueKey, CommentDataObjectForCreate commentForCreate)
    {
        if (!string.Equals(issueKey, commentForCreate.IssueKey))
        {
            return BadRequest(Models.ApiResponse.Fail(HttpStatusCode.BadRequest, ["URL's issue key and comment's issue key does not match"]));
        }

        var resultIfExists = await this.issueService.IsExistsByIdAsync(IssueUtil.GetIdFromIssueKey(issueKey));

        if (!resultIfExists.IsSuccess)
        {
            return NotFound(Models.ApiResponse.Fail(HttpStatusCode.NotFound, resultIfExists.Errors));
        }

        if (!this.ModelState.IsValid)
        {
            return BadRequestInvalidModelState();
        }

        var resultOnCreate = await this.commentService.CreateAsync(commentForCreate);
        var resultOnGet = await this.commentService.GetByIdAsync(resultOnCreate.Value);

        return ApiResponse(resultOnGet);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllByIssueKeyAsync(string issueKey)
    {
        var issueId = IssueUtil.GetIdFromIssueKey(issueKey);

        var resultOnExists = await this.issueService.IsExistsByIdAsync(issueId);

        if (!resultOnExists.IsSuccess)
        {
            return NotFound(Models.ApiResponse.Fail(HttpStatusCode.NotFound, resultOnExists.Errors));
        }

        var resultOnGet = await this.commentService.GetAllByIssueKeyAsync(issueId);
        return ApiResponse(resultOnGet);
    }
}
