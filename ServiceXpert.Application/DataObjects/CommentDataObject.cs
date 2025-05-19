namespace ServiceXpert.Application.DataObjects;
public class CommentDataObject : DataObjectBase
{
    public Guid CommentId { get; set; }

    public string Content { get; set; } = string.Empty;

    public int IssueId { get; set; }
}
