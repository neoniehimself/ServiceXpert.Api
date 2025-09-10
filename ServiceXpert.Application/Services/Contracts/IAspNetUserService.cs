using ServiceXpert.Application.DataObjects;

namespace ServiceXpert.Application.Services.Contracts;
public interface IAspNetUserService
{
    Task<(bool Succeeded, IEnumerable<string> Errors, Guid aspNetUserId)> RegisterAsync(RegisterUserDataObject dataObject);
}
