namespace Aurora.Common.Domain.Events;

public abstract class IntegrationEvent(Guid id, DateTime occurredOnUtc) : IIntegrationEvent
{
    public Guid Id { get; init; } = id;

    public DateTime OccurredOnUtc { get; init; } = occurredOnUtc;
}