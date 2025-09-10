using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.DataObjects;
public class LoginUserDataObject
{
    [Required]
    [MaxLength(256)]
    public required string UserName { get; set; }

    [Required]
    [MaxLength]
    public required string Password { get; set; }
}
