using ServiceXpert.Domain.Entities.Security;

namespace ServiceXpert.Domain.Entities.Issues;
public class Issue : EntityBase<int>
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int IssueStatusId { get; set; }

    public virtual IssueStatus? IssueStatus { get; set; }

    public int IssuePriorityId { get; set; }

    public virtual IssuePriority? IssuePriority { get; set; }

    public virtual ICollection<IssueComment> Comments { get; set; }

    public Guid? ReporterId { get; set; }

    public virtual SecurityUser? Reporter { get; set; }

    public Guid? AssigneeId { get; set; }

    public virtual SecurityUser? Assignee { get; set; }

    public virtual SecurityUser? CreatedByUser { get; set; }

    public virtual SecurityUser? ModifiedByUser { get; set; }

    public Issue()
    {
        this.Comments = [];
    }
}
