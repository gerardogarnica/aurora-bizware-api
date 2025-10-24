namespace Aurora.Common.Application.Exceptions;

public class AuroraBizSuiteException(
    string requestName,
    Error? error,
    Exception? innerException = default) : Exception($"Error processing request {requestName}.", innerException)
{
    public string RequestName { get; } = requestName;
    public Error? Error { get; } = error;
}