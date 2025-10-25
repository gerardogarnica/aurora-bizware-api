using Aurora.Common.Application.Time;
using Aurora.Common.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Aurora.Common.Infrastructure.Persistence;

public sealed class AuditableEntitiesInterceptor(
    IDateTimeService dateTimeProvider,
    IHttpContextAccessor httpContextAccessor) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            UpdateAuditableEntities(eventData.Context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateAuditableEntities(DbContext context)
    {
        var user = httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";
        var auditableEntities = context
            .ChangeTracker
            .Entries<IAuditableEntity>();

        foreach (var entry in auditableEntities)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(nameof(IAuditableEntity.CreatedBy)).CurrentValue = user;
                entry.Property(nameof(IAuditableEntity.CreatedOnUtc)).CurrentValue = dateTimeProvider.UtcNow;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(nameof(IAuditableEntity.UpdatedBy)).CurrentValue = user;
                entry.Property(nameof(IAuditableEntity.UpdatedOnUtc)).CurrentValue = dateTimeProvider.UtcNow;
            }
        }
    }
}