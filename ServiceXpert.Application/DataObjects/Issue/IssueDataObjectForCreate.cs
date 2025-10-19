using ServiceXpert.Domain.Enums.Issues;
using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.DataObjects.Issue;
public class IssueDataObjectForCreate : DataObjectBaseForCreate
{
    [Required]
    public required string Name { get; set; }

    public string? Description { get; set; }

    public int IssueStatusId { get; set; } = (int)IssueStatus.New;

    public int IssuePriorityId { get; set; } = (int)IssuePriority.Low;

    public Guid? ReporterId { get; set; }

    public Guid? AssigneeId { get; set; }
}
