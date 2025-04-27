namespace ServiceXpert.Application.DataObjects;
public class IssueStatusDataObject : DataObjectBase
{
    public int IssueStatusId { get; set; }

    public string Name { get; set; } = string.Empty;
}
