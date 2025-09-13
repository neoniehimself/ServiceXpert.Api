namespace ServiceXpert.Domain.Shared.Auditables;
public interface IModifyAuditable
{
    public Guid? ModifyUserId { get; set; }

    public DateTime? ModifyDate { get; set; }
}