using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.DataObjects.Issue;
public class IssueDataObjectForCreate : DataObjectBase
{
    [Required]
    public required string Name { get; set; }

    public string? Description { get; set; }

    public int IssueStatusId { get; set; } = (int)Domain.Shared.Enums.IssueStatus.New;

    public int IssuePriorityId { get; set; } = (int)Domain.Shared.Enums.IssuePriority.Low;
}
