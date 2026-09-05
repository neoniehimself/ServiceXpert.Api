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

    public IssueCommentController(IIssueService issueService, IIssueCommentService commentService)
    {
        this.issueService = issueService;
        this.issueCommentService = commentService;
    }

    [NonAction]
    private async Task<(bool IsSuccess, IActionResult Result)> ValidateIssueKey(string issueKey, string dataObjIssueKey, CancellationToken cancellationToken = default)
    {
        if (!string.Equals(issueKey, dataObjIssueKey))
        {
            return (false, BadRequest(Models.ApiResponse.Fail(HttpStatusCode.BadRequest, ["URL's issue key and comment's issue key does not match"])));
        }

        var resultOnExists = await this.issueService.IsExistsByIdAsync(IssueUtil.GetIdFromKey(issueKey), cancellationToken);
        if (!resultOnExists.IsSuccess)
        {
            return (false, NotFound(Models.ApiResponse.Fail(HttpStatusCode.NotFound, resultOnExists.Errors)));
        }

        if (!this.ModelState.IsValid)
        {
            return (false, BadRequestInvalidModelState());
        }

        return (true, Ok());
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(string issueKey, CreateIssueCommentDataObject createObj, CancellationToken cancellationToken = default)
    {
        var (isSuccess, result) = await ValidateIssueKey(issueKey, createObj.IssueKey, cancellationToken);
        if (!isSuccess)
        {
            return result;
        }

        var resultOnCreate = await this.issueCommentService.CreateAsync(createObj, cancellationToken);
        return ApiResponse(resultOnCreate);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllByIssueKeyAsync(string issueKey, CancellationToken cancellationToken = default)
    {
        var resultOnExists = await this.issueService.IsExistsByIdAsync(IssueUtil.GetIdFromKey(issueKey), cancellationToken);
        if (!resultOnExists.IsSuccess)
        {
            return NotFound(Models.ApiResponse.Fail(HttpStatusCode.NotFound, resultOnExists.Errors));
        }

        var resultOnGet = await this.issueCommentService.GetAllByIssueKeyAsync(issueKey, cancellationToken);
        return ApiResponse(resultOnGet);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(string issueKey, UpdateIssueCommentDataObject updateObj, CancellationToken cancellationToken = default)
    {
        var (isSuccess, result) = await ValidateIssueKey(issueKey, updateObj.IssueKey, cancellationToken);
        if (!isSuccess)
        {
            return result;
        }

        var resultOnUpdate = await this.issueCommentService.UpdateByIdAsync(updateObj.Id, updateObj, cancellationToken);
        return ApiResponse(resultOnUpdate);
    }

    [HttpDelete("{commentId}")]
    public async Task<IActionResult> DeleteByIdAsync(string issueKey, Guid commentId, CancellationToken cancellationToken = default)
    {
        var (isSuccess, result) = await ValidateIssueKey(issueKey, issueKey, cancellationToken);
        if (!isSuccess)
        {
            return result;
        }

        var resultOnDelete = await this.issueCommentService.DeleteByIdAsync(commentId, cancellationToken);
        return ApiResponse(resultOnDelete);
    }
}
