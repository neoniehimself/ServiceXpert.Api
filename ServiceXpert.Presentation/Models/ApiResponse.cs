using System.Net;

namespace ServiceXpert.Presentation.Models;
public class ApiResponse
{
    protected const string GenericErrorMessage = "One or more errors occurred.";

    public HttpStatusCode Status { get; set; }

    public bool IsSuccess { get => this.Status == HttpStatusCode.OK; }

    public string Title { get; set; } = string.Empty;

    public IReadOnlyCollection<string> ErrorMessages { get; set; } = [];

    public static ApiResponse Ok(string title = "")
    {
        return new ApiResponse
        {
            Title = title,
            Status = HttpStatusCode.OK
        };
    }

    public static ApiResponse Error(IEnumerable<string> errorMessages, string title = GenericErrorMessage, HttpStatusCode status = HttpStatusCode.InternalServerError)
    {
        return new ApiResponse
        {
            ErrorMessages = [.. errorMessages],
            Title = title,
            Status = status
        };
    }
}

public class ApiResponse<T> : ApiResponse
{
    T? Value { get; set; }

    public static ApiResponse<T?> Ok(T? value, string title = "")
    {
        return new ApiResponse<T?>
        {
            Value = value,
            Title = title,
            Status = HttpStatusCode.OK
        };
    }

    public new static ApiResponse<T?> Error(IEnumerable<string> errorMessages, string title = GenericErrorMessage, HttpStatusCode status = HttpStatusCode.InternalServerError)
    {
        return new ApiResponse<T?>
        {
            ErrorMessages = [.. errorMessages],
            Title = title,
            Status = status
        };
    }
}
