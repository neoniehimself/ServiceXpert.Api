using Microsoft.AspNetCore.Identity;
using ServiceXpert.Application.Enums;
using ServiceXpert.Application.Models;
using ServiceXpert.Application.Services.Contracts.Security;
using ServiceXpert.Infrastructure.SecurityModels;

namespace ServiceXpert.Infrastructure.Services;
public class AspNetRoleService : IAspNetRoleService
{
    private readonly RoleManager<AspNetRole> roleManager;

    public AspNetRoleService(RoleManager<AspNetRole> roleManager)
    {
        this.roleManager = roleManager;
    }

    public async Task<ServiceResult> CreateRoleAsync(string roleName)
    {
        if (!await this.roleManager.RoleExistsAsync(roleName))
        {
            var result = await this.roleManager.CreateAsync(new AspNetRole(roleName));

            return result.Succeeded
                ? ServiceResult.Ok()
                : ServiceResult.Fail(ServiceResultStatus.InternalError, result.Errors.Select(e => e.Description));
        }

        return ServiceResult.Fail(ServiceResultStatus.ValidationError, ["Role already exists!"]);
    }
}
