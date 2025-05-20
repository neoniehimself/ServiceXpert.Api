using ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Application.DataObjects.Comment;
public class CommentDataObject : DataObjectBase
{
    public Guid CommentId { get; set; }

    public string Content { get; set; } = string.Empty;

    public string IssueKey { get => string.Concat(nameof(IssuePreFix.SXP), '-', this.IssueId); }

    public int IssueId { get; set; }
}
