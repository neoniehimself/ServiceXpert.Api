using ServiceXpert.Domain.Audits;

namespace ServiceXpert.Application.DataObjects;
public abstract class UpdateDataObjectBase : IModificationAudit
{
    public Guid? ModifiedByUserId { get; set; }

    public DateTimeOffset? ModifiedDate { get; set; }
}
