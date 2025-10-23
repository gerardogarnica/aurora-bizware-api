using Aurora.Common.Domain.Results;

namespace Aurora.Common.Domain.Values;

public record WebUri
{
    public string Value { get; init; }

    private WebUri(string value) => Value = value;

    public static Result<WebUri> Create(string uri)
    {
        if (uri == null)
        {
            return Result.Fail<WebUri>(ValueErrors.InvalidWebSiteAddress);
        }

        if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
        {
            return Result.Fail<WebUri>(ValueErrors.InvalidWebSiteAddress);
        }

        return new WebUri(uri);
    }
}