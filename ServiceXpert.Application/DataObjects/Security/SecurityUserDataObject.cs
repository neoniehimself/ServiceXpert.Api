namespace ServiceXpert.Application.DataObjects.Security;
public class SecurityUserDataObject : DataObjectBase<Guid>
{
    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public SecurityProfileDataObject? SecurityProfile { get; set; }
}
