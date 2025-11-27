using Aurora.Modules.Identity.Domain.Roles;

namespace Aurora.Modules.Identity.Domain.Users;

public sealed class User : AggregateRoot<UserId>, IAuditableEntity
{
    private readonly List<Role> _roles = [];

    public Email Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string FullName => $"{FirstName} {LastName}";
    public string IdentityId { get; private set; }
    public UserStatus Status { get; private set; }
    public string CreatedBy { get; init; }
    public DateTime CreatedOnUtc { get; init; }
    public string? UpdatedBy { get; init; }
    public DateTime? UpdatedOnUtc { get; init; }
    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();

    private User() : base(new UserId(Guid.CreateVersion7())) { }

    public static User Create(
        Email email,
        string firstName,
        string lastName,
        string identityId)
    {
        User user = new()
        {
            Email = email,
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            IdentityId = identityId,
            Status = UserStatus.Draft
        };

        user.AddDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }

    public Result<User> Update(
        string firstName,
        string lastName)
    {
        if (Status is not UserStatus.Active)
        {
            return Result.Fail<User>(UserErrors.UserIsNotActive);
        }

        FirstName = firstName.Trim();
        LastName = lastName.Trim();

        AddDomainEvent(new UserUpdatedDomainEvent(Id, FullName));

        return this;
    }

    public Result<User> Activate()
    {
        if (Status is not UserStatus.Draft)
        {
            return Result.Fail<User>(UserErrors.UserShouldBeDraft);
        }

        Status = UserStatus.Active;

        AddDomainEvent(new UserActivatedDomainEvent(Id));

        return this;
    }

    public Result<User> Suspend()
    {
        if (Status is not UserStatus.Active)
        {
            return Result.Fail<User>(UserErrors.UserIsNotActive);
        }

        Status = UserStatus.Suspended;

        AddDomainEvent(new UserSuspendedDomainEvent(Id));

        return this;
    }

    public Result<User> Delete()
    {
        Status = UserStatus.Deleted;

        AddDomainEvent(new UserDeletedDomainEvent(Id));

        return this;
    }

    public Result<User> AssignRole(Role role)
    {
        if (Status is not UserStatus.Active)
        {
            return Result.Fail<User>(UserErrors.UserIsNotActive);
        }

        if (role.IsDeleted)
        {
            return Result.Fail<User>(RoleErrors.IsDeleted);
        }

        if (_roles.Contains(role))
        {
            return Result.Fail<User>(UserErrors.RoleAlreadyAssigned);
        }

        _roles.Add(role);

        return this;
    }

    public Result<User> RemoveRole(Role role)
    {
        if (!_roles.Contains(role))
        {
            return Result.Fail<User>(UserErrors.RoleNotAssigned);
        }

        if (Status is not UserStatus.Active)
        {
            return Result.Fail<User>(UserErrors.UserIsNotActive);
        }

        _roles.Remove(role);

        return this;
    }
}