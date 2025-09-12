using ServiceXpert.Domain.Shared.Audits;

namespace ServiceXpert.Application.DataObjects;
public abstract class DataObjectBase : IAuditable
{
    public Guid CreateUserId { get; set; }

    public DateTime CreateDate { get; set; }

    public Guid ModifyUserId { get; set; }

    public DateTime ModifyDate { get; set; }
}
