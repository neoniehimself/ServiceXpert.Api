namespace ServiceXpert.Domain.Shared.Audits;
public interface IAuditable
{
    public Guid CreateUserId { get; set; }

    public DateTime CreateDate { get; set; }

    public Guid ModifyUserId { get; set; }

    public DateTime ModifyDate { get; set; }
}
