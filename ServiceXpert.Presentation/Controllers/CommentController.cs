using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects.Comment;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Application.Shared.Utils;
using ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Presentation.Controllers;
[Authorize(Policy = nameof(Policy.AdminOrUser))]
[Route("Api/Issues/{issueKey}/Comments")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService commentService;
    private readonly IIssueService issueService;

    private string CommentControllerFullUriFormat { get => "api/issues/{0}/comments/{1}"; }

    public CommentController(ICommentService commentService, IIssueService issueService)
    {
        this.commentService = commentService;
        this.issueService = issueService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(string issueKey, CommentDataObjectForCreate commentForCreate)
    {
        if (!string.Equals(issueKey, commentForCreate.IssueKey))
        {
            return BadRequest("URL.IssueKey and Comment.IssueKey does not match");
        }

        if (!await this.issueService.IsExistsByIdAsync(IssueUtil.GetIdFromIssueKey(issueKey)))
        {
            return NotFound($"No such issue exists. IssueKey: {issueKey}");
        }

        if (!this.ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        var commentId = await this.commentService.CreateAsync(commentForCreate);
        var comment = await this.commentService.GetByIdAsync(commentId);
        return Created(string.Format(this.CommentControllerFullUriFormat, comment!.IssueKey, commentId), comment);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentDataObject>>> GetAllByIssueKeyAsync(string issueKey)
    {
        var issueId = IssueUtil.GetIdFromIssueKey(issueKey);

        if (!await this.issueService.IsExistsByIdAsync(issueId))
        {
            return NotFound($"No such issue exists. IssueKey: {issueKey}");
        }

        var comments = await this.commentService.GetAllByIssueKeyAsync(issueId);
        return Ok(comments);
    }

    [HttpGet("{commentId:guid}")]
    public async Task<ActionResult> GetByIdAsync(string issueKey, Guid commentId)
    {
        var issueId = IssueUtil.GetIdFromIssueKey(issueKey);

        if (!await this.issueService.IsExistsByIdAsync(issueId))
        {
            return NotFound($"No such issue exists. IssueKey: {issueKey}");
        }

        var comment = await this.commentService.GetByIdAsync(commentId);
        return comment != null ? Ok(comment) : NotFound(commentId);
    }
}
