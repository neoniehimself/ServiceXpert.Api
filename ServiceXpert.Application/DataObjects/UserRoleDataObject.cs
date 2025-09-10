namespace ServiceXpert.Application.DataObjects;
public class UserRoleDataObject
{
    /// <summary>
    /// User's UserName
    /// </summary>
    public required string UserName { get; set; }

    public required string RoleName { get; set; }
}
