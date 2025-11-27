namespace Aurora.Modules.Identity.Domain.Roles;

public sealed class Role : AggregateRoot<RoleId>, IAuditableEntity, ISoftDeletableEntity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string? Notes { get; private set; }
    public bool IsSystemRole { get; private set; }
    public string CreatedBy { get; init; }
    public DateTime CreatedOnUtc { get; init; }
    public string? UpdatedBy { get; init; }
    public DateTime? UpdatedOnUtc { get; init; }
    public bool IsDeleted { get; init; }
    public string? DeletedBy { get; init; }
    public DateTime? DeletedOnUtc { get; init; }

    private Role() : base(new RoleId(Guid.CreateVersion7())) { }

    public static Role Create(
        string name,
        string? description,
        string? notes,
        bool isSystemRole)
    {
        Role role = new()
        {
            Name = name.Trim(),
            Description = description?.Trim(),
            Notes = notes?.Trim(),
            IsSystemRole = isSystemRole
        };

        return role;
    }

    public Result<Role> Update(
        string name,
        string? description,
        string? notes)
    {
        if (IsDeleted)
        {
            return Result.Fail<Role>(RoleErrors.IsDeleted);
        }

        Name = name.Trim();
        Description = description?.Trim();
        Notes = notes?.Trim();

        return this;
    }

    public Result<Role> Delete()
    {
        if (IsDeleted)
        {
            return Result.Fail<Role>(RoleErrors.IsDeleted);
        }

        return this;
    }
}