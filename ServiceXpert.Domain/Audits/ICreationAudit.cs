namespace ServiceXpert.Domain.Audits;
public interface ICreationAudit
{
    public Guid CreatedByUserId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
}
