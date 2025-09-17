using Microsoft.AspNetCore.Identity;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Infrastructure.SecurityModels;

namespace ServiceXpert.Infrastructure.Services;
public class AspNetRoleService : IAspNetRoleService
{
    private readonly RoleManager<AspNetRole> roleManager;

    public AspNetRoleService(RoleManager<AspNetRole> roleManager)
    {
        this.roleManager = roleManager;
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
