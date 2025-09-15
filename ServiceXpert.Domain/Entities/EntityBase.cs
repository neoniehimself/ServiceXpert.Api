using ServiceXpert.Domain.Shared.Audits;

namespace ServiceXpert.Domain.Entities;
public abstract class EntityBase<TId> : IAudit
{
    public TId Id { get; set; } = default!;

    public Guid CreatedByUserId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public Guid? ModifiedByUserId { get; set; }

    public DateTimeOffset? ModifiedDate { get; set; }
}
