namespace ServiceXpert.Application.DataObjects;
public class IssuePriorityDataObject : DataObjectBase
{
    public int IssuePriorityId { get; set; }

    public string Name { get; set; } = string.Empty;
}
