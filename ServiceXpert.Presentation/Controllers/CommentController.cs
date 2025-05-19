using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Application.Services.Contracts;

namespace ServiceXpert.Presentation.Controllers;
[Route("api/issues/{issueId}/comments")]
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
    public async Task<IActionResult> GetAllAsync(int issueId)
    {
        if (!await this.issueService.IsExistsByIdAsync(issueId))
        {
            return NotFound($"No such issue exists. IssueId: {issueId}");
        }

        var comments = await this.commentService.GetAllAsync(c => c.IssueId == issueId);
        return Ok(comments);
    }

    [HttpGet("{commentId}")]
    public async Task<IActionResult> GetByIdAsync(int issueId, Guid commentId)
    {
        if (!await this.issueService.IsExistsByIdAsync(issueId))
        {
            return NotFound($"No such issue exists. IssueId: {issueId}");
        }

        var comment = await this.commentService.GetByIdAsync(commentId);
        return comment != null ? Ok(comment) : NotFound(commentId);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(int issueId, CommentDataObjectForCreate dataObject)
    {
        if (!await this.issueService.IsExistsByIdAsync(issueId))
        {
            return NotFound($"No such issue exists. IssueId: {issueId}");
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
