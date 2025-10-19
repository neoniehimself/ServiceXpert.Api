using ServiceXpert.Application.DataObjects.Security;
using ServiceXpert.Domain.Enums.Issues;

namespace ServiceXpert.Application.DataObjects.Issues;
public class IssueDataObject : DataObjectBase<int>
{
    public string IssueKey { get => string.Concat(nameof(IssuePreFix.SXP), '-', this.Id); }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int IssueStatusId { get; set; }

    public IssueStatusDataObject? IssueStatus { get; set; }

    public int IssuePriorityId { get; set; }

    public IssuePriorityDataObject? IssuePriority { get; set; }

    public ICollection<IssueCommentDataObject> Comments { get; set; }

    public Guid? ReporterId { get; set; }

    public SecurityUserDataObject? Reporter { get; set; }

    public Guid? AssigneeId { get; set; }

    public SecurityUserDataObject? Assignee { get; set; }

    public SecurityUserDataObject? CreatedByUser { get; set; }

    public virtual SecurityUserDataObject? ModifiedByUser { get; set; }

    public IssueDataObject()
    {
        this.Comments = [];
    }
}
