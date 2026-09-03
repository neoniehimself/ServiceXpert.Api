using ServiceXpert.Application.Utils;
using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.DataObjects.Issues;
public class UpdateIssueCommentDataObject : UpdateDataObjectBase
{
    public Guid Id { get; set; }

    [Required]
    public required string Content { get; set; } = string.Empty;

    [Required]
    public required string IssueKey { get; set; }

    public int IssueId { get => IssueUtil.GetIdFromKey(this.IssueKey); }
}
