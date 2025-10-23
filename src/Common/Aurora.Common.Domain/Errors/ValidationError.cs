using Aurora.Common.Domain.Results;

namespace Aurora.Common.Domain.Errors;

public sealed record ValidationError : Error
{
    public Error[] Errors { get; }

    public ValidationError(Error[] errors)
        : base("ValidationError", "One or more validation errors occurred.", ErrorType.Validation)
    {
        Errors = errors;
    }

    public static ValidationError FromResults(IEnumerable<Result> results) =>
        new([.. results.Where(r => !r.IsSuccessful).Select(r => r.Error)]);
}