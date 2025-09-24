using ServiceXpert.Application.DataObjects.Security;
using ServiceXpert.Application.Shared;

namespace ServiceXpert.Application.Services.Contracts;
public interface IAspNetUserService
{
    Task<Result<string>> LoginAsync(LoginDataObject login);

    Task<Result<Guid>> RegisterAsync(RegisterDataObject register);

    Task<Result> AssignRoleAsync(UserRoleDataObject userRole);
}
