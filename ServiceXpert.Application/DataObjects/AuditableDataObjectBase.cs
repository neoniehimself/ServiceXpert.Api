using ServiceXpert.Domain.Audits;

namespace ServiceXpert.Application.DataObjects;
public abstract class DataObjectBase<TId>
{
    public TId Id { get; set; } = default!;
}

public abstract class AuditableDataObjectBase<TId> : DataObjectBase<TId>, IAudit
{
    public Guid CreatedByUserId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public Guid? ModifiedByUserId { get; set; }

    public DateTimeOffset? ModifiedDate { get; set; }
}

public abstract class CreateDataObjectBase : ICreationAudit
{
    public Guid CreatedByUserId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
}

public abstract class UpdateDataObjectBase : IModificationAudit
{
    public Guid? ModifiedByUserId { get; set; }

    public DateTimeOffset? ModifiedDate { get; set; }
}
