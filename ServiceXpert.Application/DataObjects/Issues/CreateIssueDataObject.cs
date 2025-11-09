using ServiceXpert.Application.Extensions;
using ServiceXpert.Domain.Enums.Issues;
using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.DataObjects.Issues;
public class CreateIssueDataObject : CreateDataObjectBase
{
    [Required]
    public required string Name { get; set; }

    public string? Description { get; set; }

    public int IssueStatusId { get; set; } = IssueStatus.New.ToInt();

    public int IssuePriorityId { get; set; } = IssuePriority.Low.ToInt();

    public Guid? ReporterId { get; set; }

    public Guid? AssigneeId { get; set; }
}
