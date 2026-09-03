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

    [NonAction]
    private async Task<(bool IsSuccess, IActionResult Result)> ValidateIssueKey(string issueKey, string dataObjectIssueKey, CancellationToken cancellationToken = default)
    {
        if (!string.Equals(issueKey, dataObjectIssueKey))
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
    public async Task<IActionResult> CreateAsync(string issueKey, CreateIssueCommentDataObject createIssueComment, CancellationToken cancellationToken = default)
    {
        var validationResult = await ValidateIssueKey(issueKey, createIssueComment.IssueKey, cancellationToken);
        if (!validationResult.IsSuccess)
        {
            return validationResult.Result;
        }

        var resultOnCreate = await this.issueCommentService.CreateAsync(createIssueComment, cancellationToken);
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
    public async Task<IActionResult> UpdateAsync(string issueKey, UpdateIssueCommentDataObject updateIssueComment, CancellationToken cancellationToken = default)
    {
        var validationResult = await ValidateIssueKey(issueKey, updateIssueComment.IssueKey, cancellationToken);
        if (!validationResult.IsSuccess)
        {
            return validationResult.Result;
        }

        var resultOnUpdate = await this.issueCommentService.UpdateByIdAsync(updateIssueComment.Id, updateIssueComment, cancellationToken);
        return ApiResponse(resultOnUpdate);
    }

    [HttpDelete("{issueCommentId}")]
    public async Task<IActionResult> DeleteByIdAsync(string issueKey, Guid issueCommentId, CancellationToken cancellationToken = default)
    {
        var validationResult = await ValidateIssueKey(issueKey, issueKey, cancellationToken);
        if (!validationResult.IsSuccess)
        {
            return validationResult.Result;
        }

        var resultOnDelete = await this.issueCommentService.DeleteByIdAsync(issueCommentId, cancellationToken);
        return ApiResponse(resultOnDelete);
    }
}
