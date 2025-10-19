namespace ServiceXpert.Application.DataObjects.Security;
public class SecurityUserDataObject : AuditableDataObjectBase<Guid>
{
    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public SecurityProfileDataObject? SecurityProfile { get; set; }
}
