using Microsoft.AspNetCore.Identity;
using ServiceXpert.Application.DataObjects.Auth;
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

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> AssignRoleAsync(UserRoleDataObject userRole)
    {
        var user = await this.userManager.FindByNameAsync(userRole.UserName);

        if (user == null)
        {
            return (false, new List<string> { "User not found!" });
        }

        if (!await this.roleManager.RoleExistsAsync(userRole.RoleName))
        {
            return (false, new List<string> { "Role not found!" });
        }

        if (await this.userManager.IsInRoleAsync(user, userRole.RoleName))
        {
            return (false, new List<string> { "User already assigned to this role!" });
        }

        var result = await this.userManager.AddToRoleAsync(user, userRole.RoleName);
        return result.Succeeded ? (true, []) : (false, result.Errors.Select(e => e.Description));
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> CreateRoleAsync(string roleName)
    {
        if (!await this.roleManager.RoleExistsAsync(roleName))
        {
            var result = await this.roleManager.CreateAsync(new AspNetRole(roleName));
            return result.Succeeded ? (true, []) : (false, result.Errors.Select(e => e.Description));
        }

        return (false, new List<string> { "Role already exists!" });
    }
}
