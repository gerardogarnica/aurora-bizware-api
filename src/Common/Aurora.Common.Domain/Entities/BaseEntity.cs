namespace Aurora.Common.Domain.Entities;

public abstract class BaseEntity<TId>
{
    public TId Id { get; init; }

    protected BaseEntity()
    {
        Id = default!;
    }

    protected BaseEntity(TId id)
    {
        Id = id;
    }
}