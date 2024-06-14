namespace Event.Api.Shared;

public class Result
{
    protected Result(bool succeded, Error error)
    {
        switch (succeded)
        {
            case true when error != Error.None:
                throw new InvalidOperationException();
            case false when error == Error.None:
                throw new InvalidOperationException();
            default:
                Succeded = succeded;
                Error = error;
                break;
        }
    }

    public bool Succeded { get; }

    public bool IsFailure => !Succeded;

    public Error Error { get; }

    public static Result Success()
    {
        return new Result(true, Error.None);
    }

    public static Result<TValue> Success<TValue>(TValue value)
    {
        return new Result<TValue>(value, true, Error.None);
    }

    public static Result Failure(Error error)
    {
        return new Result(false, error);
    }

    public static Result<TValue> Failure<TValue>(Error error)
    {
        return new Result<TValue>(default, false, error);
    }

    protected static Result Create(bool condition)
    {
        return condition ? Success() : Failure(Error.ConditionFailed);
    }

    protected static Result<TValue> Create<TValue>(TValue? value)
    {
        return value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
    }
}