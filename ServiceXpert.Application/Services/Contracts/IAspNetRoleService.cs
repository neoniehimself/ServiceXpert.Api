using ServiceXpert.Application.DataObjects;

namespace ServiceXpert.Application.Services.Contracts;
public interface IAspNetRoleService
{
    Task<(bool Succeeded, IEnumerable<string> Errors)> AssignRoleAsync(UserRoleDataObject dataObject);

    Task<(bool Succeeded, IEnumerable<string> Errors)> CreateRoleAsync(string roleName);
}
