using ServiceXpert.Domain.Shared.Audits;

namespace ServiceXpert.Domain.Entities;
public abstract class EntityBase<TId> : IAudit
{
    public TId Id { get; set; } = default!;

    public Guid CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
