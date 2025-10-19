using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.DataObjects.Security;
public class CreateSecurityProfileDataObject : CreateDataObjectBase
{
    [Required]
    public Guid Id { get; }

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    public CreateSecurityProfileDataObject(Guid id)
    {
        this.Id = id;
    }
}
