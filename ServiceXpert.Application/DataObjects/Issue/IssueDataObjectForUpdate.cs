using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.DataObjects.Issue;
public class IssueDataObjectForUpdate : DataObjectBase
{
    [Required]
    [MaxLength(256)]
    public required string Name { get; set; }

    [MaxLength(4096)]
    public string? Description { get; set; }

    public required int IssueStatusId { get; set; }

    public required int IssuePriorityId { get; set; }
}
