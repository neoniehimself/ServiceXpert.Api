using ServiceXpert.Domain.Audits;

namespace ServiceXpert.Application.DataObjects;

public abstract class AuditableDataObjectBase<TId> : DataObjectBase<TId>, IAudit
{
    public Guid CreatedByUserId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public Guid? ModifiedByUserId { get; set; }

    public DateTimeOffset? ModifiedDate { get; set; }
}
