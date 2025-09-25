using ServiceXpert.Application.Shared;

namespace ServiceXpert.Application.Services.Contracts;
public interface IAspNetRoleService
{
    Task<Result> CreateRoleAsync(string roleName);
}
