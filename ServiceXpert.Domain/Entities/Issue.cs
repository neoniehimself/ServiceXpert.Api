namespace ServiceXpert.Domain.Entities;
public class Issue : EntityBase<int>
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int IssueStatusId { get; set; }

    public virtual IssueStatus? IssueStatus { get; set; }

    public int IssuePriorityId { get; set; }

    public virtual IssuePriority? IssuePriority { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }

    public virtual AspNetUserProfile? CreatedByUser { get; set; }

    public Guid? AssigneeId { get; set; }

    public virtual AspNetUserProfile? Assignee { get; set; }

    public virtual AspNetUserProfile? ModifiedByUser { get; set; }

    public Issue()
    {
        this.Comments = [];
    }
}
