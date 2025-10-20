namespace ServiceXpert.Application.DataObjects.Security;
public class SecurityUserDataObject : DataObjectBase<Guid>
{
    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public string? PhoneNumber { get; set; }

    public bool IsActive { get; set; }

    public SecurityProfileDataObject? SecurityProfile { get; set; }
}
