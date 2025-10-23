using Aurora.Common.Domain.Results;

namespace Aurora.Common.Domain.Errors;

public record Error(string Code, string Message, ErrorType Type)
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);
    public static readonly Error NullValue = new("General.NullValue", "The result value is null.", ErrorType.Failure);

    public static implicit operator Result(Error error) => Result.Fail(error);

    public static Error Failure(string code, string message) => new(code, message, ErrorType.Failure);

    public static Error Validation(string code, string message) => new(code, message, ErrorType.Validation);

    public static Error Problem(string code, string message) => new(code, message, ErrorType.Problem);

    public static Error NotFound(string code, string message) => new(code, message, ErrorType.NotFound);

    public static Error Conflict(string code, string message) => new(code, message, ErrorType.Conflict);
}