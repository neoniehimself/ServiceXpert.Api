using ServiceXpert.Domain.Shared.Audits;

namespace ServiceXpert.Application.DataObjects;
public abstract class DataObjectBase : IDate
{
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;

    public DateTime ModifyDate { get; set; } = DateTime.UtcNow;
}
