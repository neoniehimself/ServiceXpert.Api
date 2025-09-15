namespace ServiceXpert.Domain.Shared.Audits;
public interface ICreationAudit
{
    public Guid CreatedByUserId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
}
