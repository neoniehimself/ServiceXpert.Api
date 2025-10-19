using ServiceXpert.Application.DataObjects.Security;
using ServiceXpert.Domain.Enums.Issues;

namespace ServiceXpert.Application.DataObjects.Issues;
public class IssueCommentDataObject : DataObjectBase<Guid>
{
    public string Content { get; set; } = string.Empty;

    public string IssueKey { get => string.Concat(nameof(IssuePreFix.SXP), '-', this.IssueId); }

    public int IssueId { get; set; }

    public SecurityUserDataObject? CreatedByUser { get; set; }
}
