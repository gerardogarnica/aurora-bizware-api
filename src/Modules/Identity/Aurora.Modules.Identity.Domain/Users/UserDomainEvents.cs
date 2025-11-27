namespace Aurora.Modules.Identity.Domain.Users;

public sealed class UserActivatedDomainEvent(UserId userId) : DomainEvent
{
    public UserId UserId { get; init; } = userId;
}

public sealed class UserCreatedDomainEvent(UserId userId) : DomainEvent
{
    public UserId UserId { get; init; } = userId;
}

public sealed class UserDeletedDomainEvent(UserId userId) : DomainEvent
{
    public UserId UserId { get; init; } = userId;
}

public sealed class UserSuspendedDomainEvent(UserId userId) : DomainEvent
{
    public UserId UserId { get; init; } = userId;
}

public sealed class UserUpdatedDomainEvent(UserId userId, string fullName) : DomainEvent
{
    public UserId UserId { get; init; } = userId;
    public string FullName { get; init; } = fullName;
}