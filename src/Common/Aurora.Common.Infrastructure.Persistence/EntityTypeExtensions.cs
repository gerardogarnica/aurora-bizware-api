using Aurora.Common.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aurora.Common.Infrastructure.Persistence;

public static class EntityTypeExtensions
{
    public static void AddAuditableProperties<T>(
        this EntityTypeBuilder<T> builder) where T : class, IAuditableEntity
    {
        builder.Property(p => p.CreatedBy).IsRequired().HasMaxLength(100);
        builder.Property(p => p.CreatedOnUtc).IsRequired();
        builder.Property(p => p.UpdatedBy).HasMaxLength(100);
        builder.Property(p => p.UpdatedOnUtc);
    }

    public static void AddSoftDeletableProperties<T>(
        this EntityTypeBuilder<T> builder) where T : class, ISoftDeletableEntity
    {
        builder.Property(p => p.IsDeleted).IsRequired();
        builder.Property(p => p.DeletedBy).HasMaxLength(100);
        builder.Property(p => p.DeletedOnUtc);
    }
}