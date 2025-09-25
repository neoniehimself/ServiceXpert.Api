using System.Net;

namespace ServiceXpert.Presentation.Models;
public class ApiResponse
{
    public HttpStatusCode Status { get; set; }

    public bool IsSuccess { get => this.Status == HttpStatusCode.OK; }

    public IReadOnlyCollection<string> Errors { get; set; } = [];

    public static ApiResponse Ok()
    {
        return new ApiResponse
        {
            Status = HttpStatusCode.OK
        };
    }

    public static ApiResponse Fail(HttpStatusCode status, IEnumerable<string> errors)
    {
        return new ApiResponse
        {
            Status = status,
            Errors = [.. errors]
        };
    }
}

public class ApiResponse<T> : ApiResponse
{
    public T Value { get; set; } = default!;

    public static ApiResponse<T> Ok(T value)
    {
        return new ApiResponse<T>
        {
            Value = value,
            Status = HttpStatusCode.OK,
        };
    }

    public new static ApiResponse<T> Fail(HttpStatusCode status, IEnumerable<string> errors)
    {
        return new ApiResponse<T>
        {
            Status = status,
            Errors = [.. errors],
        };
    }
}
