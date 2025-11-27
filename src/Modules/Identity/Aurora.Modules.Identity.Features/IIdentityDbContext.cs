namespace Aurora.Modules.Identity.Features;

public interface IIdentityDbContext : IDbContext
{
    DbSet<Permission> Permissions { get; }
    DbSet<Role> Roles { get; }
    DbSet<User> Users { get; }
}