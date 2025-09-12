namespace ServiceXpert.Application.DataObjects.AspNetUserProfile;
public class AspNetUserProfileDataObject : DataObjectBase
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
}
