using Aurora.Common.Domain.Errors;
using Aurora.Common.Domain.Results;
using Microsoft.AspNetCore.Http;

namespace Aurora.Common.Endpoints;

public static class ApiResponses
{
    public static IResult Problem(Result result)
    {
        if (result.IsSuccessful)
        {
            throw new InvalidOperationException("The result is successful. Can't convert succesful result to a problem.");
        }

        return Results.Problem(
            title: $"The result of the request is a {result.Error.Code} failure",
            detail: result.Error.Message,
            type: GetTypeFromError(result.Error.Type),
            statusCode: GetStatusCodeFromError(result.Error.Type),
            extensions: GetErrors(result)
        );
    }

    private static string GetTypeFromError(ErrorType errorType) => errorType switch
    {
        ErrorType.Failure => "https://tools.ietf.org/html/rfc7231#section-6.6.1",
        ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
        ErrorType.Problem => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
        ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
        ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
        _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
    };

    private static int GetStatusCodeFromError(ErrorType errorType) => errorType switch
    {
        ErrorType.Failure => StatusCodes.Status500InternalServerError,
        ErrorType.Validation => StatusCodes.Status400BadRequest,
        ErrorType.Problem => StatusCodes.Status400BadRequest,
        ErrorType.NotFound => StatusCodes.Status404NotFound,
        ErrorType.Conflict => StatusCodes.Status409Conflict,
        _ => StatusCodes.Status500InternalServerError
    };

    private static Dictionary<string, object?>? GetErrors(Result result)
    {
        if (result.Error is not ValidationError validationError)
        {
            return null;
        }

        return new Dictionary<string, object?>
        {
            { "errors", validationError.Errors }
        };
    }
}