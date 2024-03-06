namespace MyBudget.SharedKernel;

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }


    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }


    public static Result Success() => new(true, Error.None);

    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

    public static Result Failure(Error error) => new(false, error);
    public static implicit operator Result(Error error) => new(false, error);

    public TResult Match<TResult>(
        Func<TResult> success,
        Func<Error, TResult> failure
    ) => IsSuccess ? success() : failure(Error);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error)

    {
        _value = value;
    }

    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public static implicit operator Result<TValue>(TValue? value) => new(value, true, Error.None);

    public static implicit operator Result<TValue>(Error error) => new(default, false, error);

    public TResult Match<TResult>(
        Func<TValue, TResult> success,
        Func<Error, TResult> failure
    ) => IsSuccess ? success(_value!) : failure(Error);
}