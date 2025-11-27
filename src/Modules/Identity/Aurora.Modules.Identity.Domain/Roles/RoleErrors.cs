namespace Aurora.Modules.Identity.Domain.Roles;

public static class RoleErrors
{
    public static readonly Error RoleNotFound = Error.NotFound(
        "Role.NotFound",
        "The specified role was not found.");

    public static readonly Error IsDeleted = Error.Validation(
        "Role.IsDeleted",
        "The specified role is deleted and cannot perform this operation.");

    public static readonly Error NameAlreadyExists = Error.Conflict(
        "Role.NameAlreadyExists",
        "A role with the specified name already exists.");
}