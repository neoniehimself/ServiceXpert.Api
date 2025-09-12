using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.DataObjects.Auth;
public class RegisterUserDataObject
{
    [Required]
    public required string UserName { get; set; }

    [Required]
    public required string Password { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }
}
