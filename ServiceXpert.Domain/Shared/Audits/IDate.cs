namespace ServiceXpert.Domain.Shared.Audits;
public interface IDate
{
    public DateTime CreateDate { get; set; }

    public DateTime ModifyDate { get; set; }
}
