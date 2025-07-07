using ServiceXpert.Domain.Utils;
using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.DataObjects.Comment;
public class CommentDataObjectForCreate : DataObjectBase
{
    [Required]
    [MaxLength]
    public required string Content { get; set; } = string.Empty;

    [Required]
    [MaxLength(7)] // SXP-999
    public required string IssueKey { get; set; }

    public int IssueId { get => IssueUtil.GetIdFromIssueKey(this.IssueKey); }
}
