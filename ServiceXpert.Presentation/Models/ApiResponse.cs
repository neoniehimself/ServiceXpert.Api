using System.Net;

namespace ServiceXpert.Presentation.Models;
public class ApiResponse
{
    public HttpStatusCode StatusCode { get; set; }

    public bool IsSuccess { get => this.StatusCode == HttpStatusCode.OK; }

    public IReadOnlyCollection<string> Errors { get; set; } = [];

    public static ApiResponse Ok()
    {
        return new ApiResponse
        {
            StatusCode = HttpStatusCode.OK
        };
    }

    public static ApiResponse Fail(HttpStatusCode statusCode, IEnumerable<string> errors)
    {
        return new ApiResponse
        {
            StatusCode = statusCode,
            Errors = [.. errors]
        };
    }
}
