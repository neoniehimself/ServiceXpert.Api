using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.Models.Auth;
public class LoginUser
{
    [Required]
    public required string UserName { get; set; }

    [Required]
    public required string Password { get; set; }
}
