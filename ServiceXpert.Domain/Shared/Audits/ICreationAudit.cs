namespace ServiceXpert.Domain.Shared.Audits;
public interface ICreationAudit
{
    public Guid CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }
}
