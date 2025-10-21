using ServiceXpert.Application.Utils;
using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.DataObjects.Issues;
public class CreateIssueCommentDataObject : CreateDataObjectBase
{
    [Required]
    public required string Content { get; set; } = string.Empty;

    [Required]
    public required string IssueKey { get; set; }

    public int IssueId { get => IssueUtil.GetIdFromKey(this.IssueKey); }
}
