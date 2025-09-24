using ServiceXpert.Application.Shared.Enums;

namespace ServiceXpert.Application.Shared;
public class Result
{
    protected const string GenericErrorMessage = "One or more errors occurred.";

    public ResultStatus Status { get; init; }

    public bool IsSuccess { get => this.Status == ResultStatus.Success; }

    public string Title { get; set; } = string.Empty;

    public IReadOnlyCollection<string> Errors { get; set; } = [];

    public static Result Ok(string title = "")
    {
        return new Result
        {
            Title = title,
            Status = ResultStatus.Success
        };
    }

    public static Result Fail(ResultStatus status, string title = GenericErrorMessage)
    {
        return new Result
        {
            Status = status,
            Title = title
        };
    }

    public static Result Fail(ResultStatus status, IEnumerable<string> errors, string title = GenericErrorMessage)
    {
        return new Result
        {
            Status = status,
            Errors = [.. errors],
            Title = title
        };
    }
}

public class Result<T> : Result
{
    T Value { get; set; } = default!;

    public static Result<T> Ok(T value, string title = "")
    {
        return new Result<T>
        {
            Value = value,
            Title = title,
            Status = ResultStatus.Success
        };
    }

    public new static Result<T> Fail(ResultStatus status, string title = GenericErrorMessage)
    {
        return new Result<T>
        {
            Status = status,
            Title = title
        };
    }

    public new static Result<T> Fail(ResultStatus status, IEnumerable<string> errors, string title = GenericErrorMessage)
    {
        return new Result<T>
        {
            Status = status,
            Errors = [.. errors],
            Title = title
        };
    }
}