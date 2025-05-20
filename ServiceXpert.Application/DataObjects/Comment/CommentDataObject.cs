namespace ServiceXpert.Application.DataObjects.Comment;
public class CommentDataObject : DataObjectBase
{
    public Guid CommentId { get; set; }

    public string Content { get; set; } = string.Empty;

    public int IssueId { get; set; }
}
