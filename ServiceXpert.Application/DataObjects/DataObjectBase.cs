using ServiceXpert.Domain.Shared.Audits

namespace ServiceXpert.Application.DataObjects;
public abstract class DataObjectBase : IAudit
{
    public Guid CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifyUserId { get; set; }

    public DateTime? ModifyDate { get; set; }
}

public abstract class DataObjectBaseForCreate : ICreationAudit
{
    public Guid CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }
}

public abstract class DataObjectBaseForUpdate : IModificationAudit
{
    public Guid? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
