namespace Aurora.Modules.Identity.Domain.Users;

public static class UserErrors
{
    public static readonly Error UserNotFound = Error.NotFound(
        "User.NotFound",
        "The specified user was not found.");

    public static readonly Error EmailAlreadyExists = Error.Conflict(
        "User.EmailAlreadyExists",
        "The specified email address is already in use by another user.");

    public static readonly Error RoleAlreadyAssigned = Error.Validation(
        "User.RoleAlreadyAssigned",
        "The specified role is already assigned to the user.");

    public static readonly Error RoleNotAssigned = Error.Validation(
        "User.RoleNotAssigned",
        "The specified role is not assigned to the user.");

    public static readonly Error UserIsNotActive = Error.Validation(
        "User.UserIsNotActive",
        "The user is not active and cannot perform this operation.");

    public static readonly Error UserShouldBeDraft = Error.Validation(
        "User.UserShouldBeDraft",
        "The user should be in draft status to perform this operation.");
}