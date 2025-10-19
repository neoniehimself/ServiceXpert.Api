using ServiceXpert.Domain.Audits;

namespace ServiceXpert.Application.DataObjects;
public abstract class CreateDataObjectBase : ICreationAudit
{
    public Guid CreatedByUserId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
}
