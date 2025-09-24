using Microsoft.AspNetCore.Identity;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Application.Shared;
using ServiceXpert.Application.Shared.Enums;
using ServiceXpert.Infrastructure.SecurityModels;

namespace ServiceXpert.Infrastructure.Services;
public class AspNetRoleService : IAspNetRoleService
{
    private readonly RoleManager<AspNetRole> roleManager;

    public AspNetRoleService(RoleManager<AspNetRole> roleManager)
    {
        this.roleManager = roleManager;
    }

    public async Task<Result> CreateRoleAsync(string roleName)
    {
        if (!await this.roleManager.RoleExistsAsync(roleName))
        {
            var result = await this.roleManager.CreateAsync(new AspNetRole(roleName));

            return result.Succeeded
                ? Result.Ok()
                : Result.Fail(ResultStatus.InternalError, result.Errors.Select(e => e.Description));
        }

        return Result.Fail(ResultStatus.ValidationError, ["Role already exists!"]);
    }
}
