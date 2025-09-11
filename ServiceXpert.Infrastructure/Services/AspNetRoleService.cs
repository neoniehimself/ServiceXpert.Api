using Microsoft.AspNetCore.Identity;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Infrastructure.AuthModels;

namespace ServiceXpert.Infrastructure.Services;
public class AspNetRoleService : IAspNetRoleService
{
    private readonly UserManager<AspNetUser> userManager;
    private readonly RoleManager<AspNetRole> roleManager;

    public AspNetRoleService(UserManager<AspNetUser> userManager, RoleManager<AspNetRole> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> AssignRoleAsync(UserRoleDataObject dataObject)
    {
        var user = await this.userManager.FindByNameAsync(dataObject.UserName);

        if (user == null)
        {
            return (false, new List<string> { "User not found!" });
        }

        if (!await this.roleManager.RoleExistsAsync(dataObject.RoleName))
        {
            return (false, new List<string> { "Role not found!" });
        }

        if (await this.userManager.IsInRoleAsync(user, dataObject.RoleName))
        {
            return (false, new List<string> { "User already assigned to this role!" });
        }

        var result = await this.userManager.AddToRoleAsync(user, dataObject.RoleName);

        return result.Succeeded ? (true, Enumerable.Empty<string>()) : (false, result.Errors.Select(e => e.Description));
    }

    public Task<(bool Succeeded, IEnumerable<string> Errors)> CreateRoleAsync(string roleName)
    {
        throw new NotImplementedException();
    }
}
