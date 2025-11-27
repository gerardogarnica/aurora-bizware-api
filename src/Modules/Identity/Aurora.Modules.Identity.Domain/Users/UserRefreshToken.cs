namespace Aurora.Modules.Identity.Domain.Users;

public sealed class UserRefreshToken
{
    public Guid Id { get; private set; }
    public UserId UserId { get; private set; }
    public string Value { get; private set; }
    public DateTime ExpiresOnUtc { get; private set; }
    public DateTime IssuedOnUtc { get; private set; }
    public User User { get; init; } = null!;

    public static UserRefreshToken Create(
        UserId userId,
        string value,
        DateTime expiresOnUtc,
        DateTime issuedOnUtc)
    {
        return new UserRefreshToken
        {
            Id = Guid.CreateVersion7(),
            UserId = userId,
            Value = value,
            ExpiresOnUtc = expiresOnUtc,
            IssuedOnUtc = issuedOnUtc
        };
    }
}