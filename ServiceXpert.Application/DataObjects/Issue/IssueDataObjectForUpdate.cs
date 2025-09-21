using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.DataObjects.Issue;
public class IssueDataObjectForUpdate : DataObjectBaseForUpdate
{
    [Required]
    public required string Name { get; set; }

    public string? Description { get; set; }

    [Required]
    public required int IssueStatusId { get; set; }

    [Required]
    public required int IssuePriorityId { get; set; }

    public Guid? ReporterId { get; set; }

    public Guid? AssigneeId { get; set; }
}
