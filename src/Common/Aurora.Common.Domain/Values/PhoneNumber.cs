using Aurora.Common.Domain.Results;
using System.Text.RegularExpressions;

namespace Aurora.Common.Domain.Values;

public readonly record struct PhoneNumber
{
    public string Code { get; }
    public string Number { get; }

    private PhoneNumber(string code, string number)
    {
        Code = code;
        Number = number;
    }

    public static Result<PhoneNumber> Create(string code, string number)
    {
        if (number == null)
        {
            return Result.Fail<PhoneNumber>(ValueErrors.InvalidPhoneNumber);
        }

        var codeRegex = @"[0-9]{1,3}";
        if (!Regex.IsMatch(code, codeRegex, RegexOptions.IgnoreCase))
        {
            return Result.Fail<PhoneNumber>(ValueErrors.InvalidPhoneNumber);
        }

        var numberRegex = @"[0-9]{3,10}";
        if (!Regex.IsMatch(number, numberRegex, RegexOptions.IgnoreCase))
        {
            return Result.Fail<PhoneNumber>(ValueErrors.InvalidPhoneNumber);
        }

        return new PhoneNumber(code, number);
    }
}