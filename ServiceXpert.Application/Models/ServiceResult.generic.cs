using ServiceXpert.Application.Enums;

namespace ServiceXpert.Application.Models;
public class ServiceResult<T> : ServiceResult
{
    public T Value { get; set; } = default!;

    public static ServiceResult<T> Ok(T value)
    {
        return new ServiceResult<T>
        {
            Value = value,
            Status = ServiceResultStatus.Success
        };
    }

    public new static ServiceResult<T> Fail(ServiceResultStatus status, IEnumerable<string> errors)
    {
        return new ServiceResult<T>
        {
            Status = status,
            Errors = [.. errors]
        };
    }
}
