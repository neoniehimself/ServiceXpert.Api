using ServiceXpert.Application.DataObjects.Auth;

namespace ServiceXpert.Application.Services.Contracts;
public interface IAspNetRoleService
{
    Task<(bool Succeeded, IEnumerable<string> Errors)> AssignRoleAsync(UserRoleDataObject userRole);

    Task<(bool Succeeded, IEnumerable<string> Errors)> CreateRoleAsync(string roleName);
}
