using EventBus.Base.Events;

namespace EventBus.Base.Abstraction
{
    public interface IIntegrationEventHandler<TEntegrationEvent> : IntegrationEventHandler where TEntegrationEvent : IntegrationEvent
    {
        Task Handle(TEntegrationEvent @event);
    }

    public interface IntegrationEventHandler
    {
    }
}
