﻿namespace Aurora.Common.Application.Events;

public interface IEventBus
{
    Task PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
        where TIntegrationEvent : IIntegrationEvent;
}