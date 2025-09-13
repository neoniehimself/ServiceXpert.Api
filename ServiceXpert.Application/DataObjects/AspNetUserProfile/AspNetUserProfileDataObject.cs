namespace ServiceXpert.Application.DataObjects.AspNetUserProfile;
public class AspNetUserProfileDataObject : DataObjectBase<Guid>
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
}
