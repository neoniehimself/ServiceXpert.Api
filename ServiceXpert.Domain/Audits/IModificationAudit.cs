namespace ServiceXpert.Domain.Audits;
public interface IModificationAudit
{
    public Guid? ModifiedByUserId { get; set; }

    public DateTimeOffset? ModifiedDate { get; set; }
}
