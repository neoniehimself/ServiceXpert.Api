using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.Models.Auth;

public class PasswordUpdate
{
    [Required]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required]
    public string NewPassword { get; set; } = string.Empty;
}
