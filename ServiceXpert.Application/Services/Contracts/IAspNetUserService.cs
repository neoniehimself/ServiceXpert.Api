using ServiceXpert.Application.DataObjects.Auth;

namespace ServiceXpert.Application.Services.Contracts;
public interface IAspNetUserService
{
    Task<(bool Succeeded, IEnumerable<string> Errors, string token)> LoginAsync(LoginUserDataObject loginUser);

    Task<(bool Succeeded, IEnumerable<string> Errors, Guid aspNetUserId)> RegisterAsync(RegisterUserDataObject registerUser);
}
