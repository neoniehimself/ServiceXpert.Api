namespace ServiceXpert.Domain.Entities;
public class Comment : EntityBase<Guid>
{
    public string Content { get; set; } = string.Empty;

    public int IssueId { get; set; }

    public AspNetUserProfile? CreatedByUser { get; set; }
}
