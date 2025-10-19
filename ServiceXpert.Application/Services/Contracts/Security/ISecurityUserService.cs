using ServiceXpert.Application.Models;
using ServiceXpert.Application.Models.Auth;

namespace ServiceXpert.Application.Services.Contracts.Security;
public interface ISecurityUserService
{
    Task<ServiceResult<string>> LoginAsync(LoginUser loginUser);

    Task<ServiceResult<Guid>> RegisterAsync(RegisterUser registerUser);

    Task<ServiceResult> AssignRoleAsync(UserRole userRole);
}
