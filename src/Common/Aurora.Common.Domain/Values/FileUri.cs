using Aurora.Common.Domain.Results;

namespace Aurora.Common.Domain.Values;

public readonly record struct FileUri
{
    public string Value { get; }

    private FileUri(string value) => Value = value;

    public static Result<FileUri> Create(string uri)
    {
        if (uri == null)
        {
            return Result.Fail<FileUri>(ValueErrors.InvalidFileUriAddress);
        }

        try
        {
            var uriAddress = new Uri(uri);

            return uriAddress.IsFile
                ? new FileUri(uri)
                : Result.Fail<FileUri>(ValueErrors.InvalidFileUriAddress);
        }
        catch (UriFormatException e)
        {
            return Result.Fail<FileUri>(ValueErrors.FileUriFormatException(e.Message));
        }
        catch (Exception e)
        {
            return Result.Fail<FileUri>(ValueErrors.FileUriGenericException(e.Message));
        }
    }
}