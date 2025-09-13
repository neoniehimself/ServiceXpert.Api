using ServiceXpert.Application.Utils;
using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.DataObjects.Comment;
public class CommentDataObjectForCreate : DataObjectBaseForCreate
{
    [Required]
    public required string Content { get; set; } = string.Empty;

    [Required]
    public required string IssueKey { get; set; }

    public int IssueId { get => IssueUtil.GetIdFromIssueKey(this.IssueKey); }
}
