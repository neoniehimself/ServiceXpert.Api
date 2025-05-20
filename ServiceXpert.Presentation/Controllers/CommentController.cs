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
    public async Task<IActionResult> GetByIdAsync(string issueKey, Guid commentId)
    {
        var issueId = IssueUtil.GetIdFromIssueKey(issueKey);
        if (!await this.issueService.IsExistsByIdAsync(issueId))
        {
            return NotFound($"No such issue exists. IssueKey: {issueKey}");
        }

        var comment = await this.commentService.GetByIdAsync(commentId);
        return comment != null ? Ok(comment) : NotFound(commentId);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(string issueKey, CommentDataObjectForCreate dataObject)
    {
        var issueId = IssueUtil.GetIdFromIssueKey(issueKey);
        if (!await this.issueService.IsExistsByIdAsync(issueId))
        {
            return NotFound($"No such issue exists. IssueKey: {issueKey}");
        }

        if (!this.ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        var commentId = await this.commentService.CreateAsync(dataObject);
        var comment = await this.commentService.GetByIdAsync(commentId);

        return Created(string.Format(this.CommentControllerFullUriFormat, issueId, commentId), comment);
    }
}
