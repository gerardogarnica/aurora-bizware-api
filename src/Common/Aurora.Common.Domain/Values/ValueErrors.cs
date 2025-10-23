using Aurora.Common.Domain.Errors;

namespace Aurora.Common.Domain.Values;

internal static class ValueErrors
{
    internal static Error FileUriFormatException(string message) => Error.Validation(
        "FileUriFormatException",
        $"The file uri address is invalid. Uri format error: {message}");

    internal static Error FileUriGenericException(string message) => Error.Validation(
        "FileUriGenericException",
        $"The file uri address is invalid. Error: {message}");

    internal static Error InvalidEmailAddress => Error.Validation(
        "InvalidEmailAddress",
        "The email address is invalid.");

    internal static Error InvalidFileUriAddress => Error.Validation(
        "InvalidFileUriAddress",
        "The file uri address is invalid.");

    internal static Error InvalidPhoneNumber => Error.Validation(
        "InvalidPhoneNumber",
        "The phone number is invalid.");

    internal static Error InvalidWebSiteAddress => Error.Validation(
        "InvalidWebSiteAddress",
        "The website address is invalid.");
}