namespace ServiceXpert.Domain.Entities;
public class Issue : EntityBase
{
    public int IssueId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int IssueStatusId { get; set; }

    public virtual IssueStatus? IssueStatus { get; set; }

    public int IssuePriorityId { get; set; }

    public virtual IssuePriority? IssuePriority { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }

    public Issue()
    {
        this.Comments = [];
    }
}
