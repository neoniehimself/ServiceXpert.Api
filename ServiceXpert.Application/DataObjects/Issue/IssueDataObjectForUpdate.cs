using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.DataObjects.Issue;
public class IssueDataObjectForUpdate : DataObjectBase
{
    [Required]
    public required string Name { get; set; }

    public string? Description { get; set; }

    [Required]
    public required int IssueStatusId { get; set; }

    [Required]
    public required int IssuePriorityId { get; set; }
}
