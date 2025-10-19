namespace ServiceXpert.Application.DataObjects.Security;
public class SecurityProfileDataObject : AuditableDataObjectBase<Guid>
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
}
