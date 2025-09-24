using ServiceXpert.Application.Shared.Enums;

namespace ServiceXpert.Application.Shared;
public class Result
{
    public ResultStatus Status { get; init; }

    public bool IsSuccess { get => this.Status == ResultStatus.Success; }

    public string Title
    {
        get => this.Status != ResultStatus.ValidationError
            ? "One or more errors occurred."
            : "One or more validation errors occurred.";
    }

    public IReadOnlyCollection<string> Errors { get; set; } = [];

    public static Result Ok()
    {
        return new Result
        {
            Status = ResultStatus.Success
        };
    }

    public static Result Fail(ResultStatus status)
    {
        return new Result
        {
            Status = status
        };
    }

    public static Result Fail(ResultStatus status, IEnumerable<string> errors)
    {
        return new Result
        {
            Status = status,
            Errors = [.. errors]
        };
    }
}

public class Result<T> : Result
{
    T Value { get; set; } = default!;

    public static Result<T> Ok(T value)
    {
        return new Result<T>
        {
            Value = value,
            Status = ResultStatus.Success
        };
    }

    public new static Result<T> Fail(ResultStatus status)
    {
        return new Result<T>
        {
            Status = status
        };
    }

    public new static Result<T> Fail(ResultStatus status, IEnumerable<string> errors)
    {
        return new Result<T>
        {
            Status = status,
            Errors = [.. errors]
        };
    }
}
