using System.Net;

namespace ServiceXpert.Presentation.Models;
public class ApiResponse<T> : ApiResponse
{
    public T Value { get; set; } = default!;

    public static ApiResponse<T> Ok(T value)
    {
        return new ApiResponse<T>
        {
            Value = value,
            StatusCode = HttpStatusCode.OK,
        };
    }

    public new static ApiResponse<T> Fail(HttpStatusCode statusCode, IEnumerable<string> errors)
    {
        return new ApiResponse<T>
        {
            StatusCode = statusCode,
            Errors = [.. errors],
        };
    }
}
