using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.DataObjects.AspNetUserProfile;
public class AspNetUserProfileDataObjectForCreate : DataObjectBaseForCreate
{
    [Required]
    public Guid Id { get; }

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    public AspNetUserProfileDataObjectForCreate(Guid id)
    {
        this.Id = id;
    }
}
