using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.DataObjects.Comment;
public class CommentDataObjectForCreate : DataObjectBase
{
    [Required]
    [MaxLength]
    public required string Content { get; set; } = string.Empty;

    public required int IssueId { get; set; }
}
