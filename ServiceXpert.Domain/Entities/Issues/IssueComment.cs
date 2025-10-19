using ServiceXpert.Domain.Entities.Security;

namespace ServiceXpert.Domain.Entities.Issues;
public class IssueComment : EntityBase<Guid>
{
    public string Content { get; set; } = string.Empty;

    public int IssueId { get; set; }

    public SecurityUser? CreatedByUser { get; set; }
}
