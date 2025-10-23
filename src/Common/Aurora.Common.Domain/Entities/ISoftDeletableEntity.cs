namespace Aurora.Common.Domain.Entities;

public interface ISoftDeletableEntity
{
    bool IsDeleted { get; }
    string? DeletedBy { get; }
    DateTime? DeletedOnUtc { get; }
}