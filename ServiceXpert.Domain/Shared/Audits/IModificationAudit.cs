namespace ServiceXpert.Domain.Shared.Audits;
public interface IModificationAudit
{
    public Guid? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }
}