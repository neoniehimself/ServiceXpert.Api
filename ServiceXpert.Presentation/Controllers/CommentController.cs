using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects.Comment;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Application.Utils;

namespace ServiceXpert.Presentation.Controllers;
[Route("api/issues/{issueKey}/comments")]
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
    public async Task<IActionResult> CreateAsync(string issueKey, CommentDataObjectForCreate dataObject)
    {
        if (!string.Equals(issueKey, dataObject.IssueKey))
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

        var commentId = await this.commentService.CreateAsync(dataObject);
        var comment = await this.commentService.GetByIdAsync(commentId);

        return Created(string.Format(this.CommentControllerFullUriFormat, comment!.IssueKey, commentId), comment);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(string issueKey)
    {
        var issueId = IssueUtil.GetIdFromIssueKey(issueKey);

        if (!await this.issueService.IsExistsByIdAsync(issueId))
        {
            return NotFound($"No such issue exists. IssueKey: {issueKey}");
        }

        var comments = await this.commentService.GetAllAsync(c => c.IssueId == issueId);
        return Ok(comments);
    }

    [HttpGet("{commentId}")]
    public async Task<IActionResult> GetAsync(string issueKey, Guid commentId)
    {
        var issueId = IssueUtil.GetIdFromIssueKey(issueKey);

        if (!await this.issueService.IsExistsByIdAsync(issueId))
        {
            return NotFound($"No such issue exists. IssueKey: {issueKey}");
        }

        var comment = await this.commentService.GetAsync(c => c.IssueId == issueId && c.CommentId == commentId);
        return comment != null ? Ok(comment) : NotFound(commentId);
    }
}
