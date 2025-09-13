using ServiceXpert.Domain.Shared.Auditables;

namespace ServiceXpert.Domain.Entities;
public abstract class EntityBase : IAuditable
{
    public Guid CreateUserId { get; set; }

    public DateTime CreateDate { get; set; }

    public Guid? ModifyUserId { get; set; }

    public DateTime? ModifyDate { get; set; }
}
