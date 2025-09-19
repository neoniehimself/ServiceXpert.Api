using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.DataObjects.Security;
public class LoginDataObject
{
    [Required]
    public required string UserName { get; set; }

    [Required]
    public required string Password { get; set; }
}
