using ServiceXpert.Application.Models;
using ServiceXpert.Application.Models.Auth;

namespace ServiceXpert.Application.Services.Contracts.Security;
public interface ISecurityUserService
{
    Task<ServiceResult<string>> LoginAsync(LoginUser login);

    Task<ServiceResult<Guid>> RegisterAsync(RegisterUser register);

    Task<ServiceResult> AssignRoleAsync(UserRole userRole);
}
