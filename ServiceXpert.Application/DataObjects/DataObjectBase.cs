namespace ServiceXpert.Application.DataObjects;
public abstract class DataObjectBase
{
    private DateTime? createDate;

    public DateTime? CreateDate
    {
        get { return this.createDate ?? DateTime.UtcNow; }
        set { this.createDate = value; }
    }


    private DateTime? modifyDate;

    public DateTime? ModifyDate
    {
        get { return this.modifyDate ?? DateTime.UtcNow; }
        set { this.modifyDate = value; }
    }
}
