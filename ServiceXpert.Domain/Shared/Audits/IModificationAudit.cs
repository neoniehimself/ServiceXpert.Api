namespace ServiceXpert.Domain.Shared.Audits;
public interface IModificationAudit
{
    public Guid? ModifiedByUserId { get; set; }

    public DateTimeOffset? ModifiedDate { get; set; }
}