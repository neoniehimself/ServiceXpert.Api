namespace ServiceXpert.Domain.Shared.Auditables;
public interface ICreateAuditable
{
    public Guid CreateUserId { get; set; }

    public DateTime CreateDate { get; set; }
}
