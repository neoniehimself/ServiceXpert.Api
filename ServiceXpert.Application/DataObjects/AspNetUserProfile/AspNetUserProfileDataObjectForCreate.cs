namespace ServiceXpert.Application.DataObjects.AspNetUserProfile;
public class AspNetUserProfileDataObjectForCreate : DataObjectBase
{
    public Guid Id { get; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public AspNetUserProfileDataObjectForCreate(Guid id)
    {
        this.Id = id;
    }
}
