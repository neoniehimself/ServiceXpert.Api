using ServiceXpert.Application.DataObjects.Security;
using ServiceXpert.Application.Models;
using ServiceXpert.Application.Models.Auth;
using ServiceXpert.Application.Models.Security.QueryOptions;
using ServiceXpert.Domain.Entities.Security;
using ServiceXpert.Domain.Helpers.Persistence.Includes;
using ServiceXpert.Domain.ValueObjects.Pagination;

namespace ServiceXpert.Application.Services.Contracts.Security;
public interface ISecurityUserService
{
    Task<ServiceResult<string>> LoginAsync(LoginUser loginUser);

    Task<ServiceResult<Guid>> RegisterAsync(RegisterUser registerUser, CancellationToken cancellationToken = default);

    Task<ServiceResult> AssignRoleAsync(UserRole userRole);

    Task<ServiceResult<PaginationResult<SecurityUserDataObject>>> GetPagedUsersAsync(GetPagedUsersQueryOption queryOption, IncludeOptions<SecurityUser>? includeOptions = null, CancellationToken cancellationToken = default);

}
