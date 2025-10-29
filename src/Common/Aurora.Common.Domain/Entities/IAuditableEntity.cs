namespace Aurora.Common.Domain.Entities;

public interface IAuditableEntity
{
    string CreatedBy { get; }
    DateTime CreatedOnUtc { get; }
    string? UpdatedBy { get; }
    DateTime? UpdatedOnUtc { get; }
}