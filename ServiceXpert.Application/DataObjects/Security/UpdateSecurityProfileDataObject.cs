using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.DataObjects.Security;

public class UpdateSecurityProfileDataObject : UpdateDataObjectBase
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;
}
