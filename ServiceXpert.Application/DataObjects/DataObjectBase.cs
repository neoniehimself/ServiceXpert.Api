using ServiceXpert.Domain.Audits;

namespace ServiceXpert.Application.DataObjects;
public abstract class DataObjectBase<TId> : IAudit
{
    public TId Id { get; set; } = default!;

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
