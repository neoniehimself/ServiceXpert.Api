using ServiceXpert.Application.Shared.Enums;

namespace ServiceXpert.Application.Shared;
public class Result
{
    protected const string GenericErrorMessage = "One or more errors occurred.";

    public ResultStatus Status { get; init; }

    public bool IsSuccess { get => this.Status == ResultStatus.Success; }

    public string Title { get; set; } = string.Empty;

    public IReadOnlyCollection<string> ErrorMessages { get; set; } = [];

    public static Result Ok(string title = "")
    {
        return new Result
        {
            Title = title,
            Status = ResultStatus.Success
        };
    }

    public static Result Error(IEnumerable<string> errorMessages, string title = GenericErrorMessage, ResultStatus status = ResultStatus.InternalError)
    {
        return new Result
        {
            ErrorMessages = [.. errorMessages],
            Title = title,
            Status = status
        };
    }
}

public class Result<T> : Result
{
    T? Value { get; set; }

    public static Result<T?> Ok(T? value, string title = "")
    {
        return new Result<T?>
        {
            Value = value,
            Title = title,
            Status = ResultStatus.Success
        };
    }

    public new static Result<T?> Error(IEnumerable<string> errorMessages, string title = GenericErrorMessage, ResultStatus status = ResultStatus.InternalError)
    {
        return new Result<T?>
        {
            ErrorMessages = [.. errorMessages],
            Title = title,
            Status = status
        };
    }
}