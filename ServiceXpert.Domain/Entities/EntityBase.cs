using ServiceXpert.Domain.Shared.Audits;

namespace ServiceXpert.Domain.Entities;
public abstract class EntityBase : IDate
{
    public DateTime CreateDate { get; set; }

    public DateTime ModifyDate { get; set; }
}
