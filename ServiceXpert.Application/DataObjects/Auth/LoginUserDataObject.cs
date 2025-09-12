using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.DataObjects.Auth;
public class LoginUserDataObject
{
    [Required]
    public required string UserName { get; set; }

    [Required]
    public required string Password { get; set; }
}
