using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.DataObjects.Issue;
public class IssueDataObjectForUpdate : DataObjectBase
{
    [Required]
    [MaxLength(256)]
    public required string Name { get; set; }

    [MaxLength(4096)]
    public string? Description { get; set; }

    [Required]
    public required int IssueStatusId { get; set; }

    [Required]
    public required int IssuePriorityId { get; set; }

    public IssueDataObjectForUpdate() : base(true)
    {
    }
}
