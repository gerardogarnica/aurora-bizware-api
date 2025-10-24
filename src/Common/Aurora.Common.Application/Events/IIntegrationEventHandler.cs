namespace Aurora.Common.Application.Events;

public interface IIntegrationEventHandler<in T> where T : IIntegrationEvent
{
    Task Handle(T integrationEvent, CancellationToken cancellationToken = default);
}