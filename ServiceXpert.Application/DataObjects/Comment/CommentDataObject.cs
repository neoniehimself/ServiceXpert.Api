using ServiceXpert.Application.DataObjects.AspNetUserProfile;
using ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Application.DataObjects.Comment;
public class CommentDataObject : DataObjectBase<Guid>
{
    public string Content { get; set; } = string.Empty;

    public string IssueKey { get => string.Concat(nameof(IssuePreFix.SXP), '-', this.IssueId); }

    public int IssueId { get; set; }

    public AspNetUserProfileDataObject? CreatedByUser { get; set; }
}
