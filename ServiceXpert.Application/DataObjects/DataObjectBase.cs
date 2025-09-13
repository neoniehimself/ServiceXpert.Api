using ServiceXpert.Domain.Shared.Auditables;

namespace ServiceXpert.Application.DataObjects;
public abstract class DataObjectBase : IAuditable
{
    public Guid CreateUserId { get; set; }

    public DateTime CreateDate { get; set; }

    public Guid? ModifyUserId { get; set; }

    public DateTime? ModifyDate { get; set; }
}

public abstract class DataObjectBaseForCreate : ICreateAuditable
{
    public Guid CreateUserId { get; set; }

    public DateTime CreateDate { get; set; }
}

public abstract class DataObjectBaseForUpdate : IModifyAuditable
{
    public Guid? ModifyUserId { get; set; }

    public DateTime? ModifyDate { get; set; }
}
