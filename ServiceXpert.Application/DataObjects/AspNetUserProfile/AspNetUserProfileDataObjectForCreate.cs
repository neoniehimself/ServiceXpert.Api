using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.DataObjects.AspNetUserProfile;
public class AspNetUserProfileDataObjectForCreate : DataObjectBase
{
    [Required]
    public Guid Id { get; }

    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    public AspNetUserProfileDataObjectForCreate(Guid id)
    {
        this.Id = id;
    }
}
