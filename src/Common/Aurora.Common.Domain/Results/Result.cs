using Aurora.Common.Domain.Errors;

namespace Aurora.Common.Domain.Results;

public class Result
{
    protected internal Result(bool isSuccessful, Error error)
    {
        if (isSuccessful && error != Error.None)
        {
            throw new ArgumentException("Result is successful, but error is not None.", nameof(error));
        }

        if (!isSuccessful && error == Error.None)
        {
            throw new ArgumentException("Result is failed, but error is None.", nameof(error));
        }

        IsSuccessful = isSuccessful;
        Error = error;
    }

    public bool IsSuccessful { get; }

    public Error Error { get; }

    public static Result Ok() => new(true, Error.None);

    public static Result<TValue> Ok<TValue>(TValue value) => new(value, true, Error.None);

    public static Result Fail(Error error) => new(false, error);

    public static Result<TValue> Fail<TValue>(Error error) => new(default!, false, error);
}