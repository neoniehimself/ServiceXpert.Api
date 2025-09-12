using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Application.DataObjects.Auth;
public class UserRoleDataObject
{
    /// <summary>
    /// User's UserName
    /// </summary>
    [Required]
    public required string UserName { get; set; }

    [Required]
    public required string RoleName { get; set; }
}
