using ServiceXpert.Application.Enums;

namespace ServiceXpert.Application.Models;
public class ServiceResult
{
    public ServiceResultStatus Status { get; init; }

    public bool IsSuccess { get => this.Status == ServiceResultStatus.Success; }

    public IReadOnlyCollection<string> Errors { get; set; } = [];

    public static ServiceResult Ok()
    {
        return new ServiceResult
        {
            Status = ServiceResultStatus.Success
        };
    }

    public static ServiceResult Fail(ServiceResultStatus status, IEnumerable<string> errors)
    {
        return new ServiceResult
        {
            Status = status,
            Errors = [.. errors]
        };
    }
}
