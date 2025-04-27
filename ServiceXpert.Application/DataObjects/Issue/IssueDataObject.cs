namespace ServiceXpert.Application.DataObjects.Issue;
public class IssueDataObject : DataObjectBase
{
    public int IssueId { get; set; }

    public string IssueKey { get => string.Concat(nameof(Domain.Shared.Enums.IssuePreFix.SXP), '-', this.IssueId); }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int IssueStatusId { get; set; }

    public IssueStatusDataObject? IssueStatus { get; set; }

    public int IssuePriorityId { get; set; }

    public IssuePriorityDataObject? IssuePriority { get; set; }
}
