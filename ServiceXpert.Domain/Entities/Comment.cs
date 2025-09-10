namespace ServiceXpert.Domain.Entities;

public class Comment : EntityBase
{
    public Guid CommentId { get; set; }

    public string Content { get; set; } = string.Empty;

    public int IssueId { get; set; }
}
