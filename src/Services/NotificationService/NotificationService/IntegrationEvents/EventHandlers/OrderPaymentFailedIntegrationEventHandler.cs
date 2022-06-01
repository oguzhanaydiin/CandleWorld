using EventBus.Base.Abstraction;
using Microsoft.Extensions.Logging;
using PaymentService.Api.IntegrationEvents.Events;

namespace NotificationService.IntegrationEvents.EventHandlers;

public class OrderPaymentFailedIntegrationEventHandler : IIntegrationEventHandler<OrderPaymentFailedIntegrationEvent>
{
    private readonly ILogger<OrderPaymentFailedIntegrationEventHandler> logger;

    public OrderPaymentFailedIntegrationEventHandler(ILogger<OrderPaymentFailedIntegrationEventHandler> logger)
    {
        this.logger = logger;
    }

    public Task Handle(OrderPaymentFailedIntegrationEvent @event)
    {
        //Send Fail Notification (SMS, Email, Push)

        logger.LogInformation($" OrderPayment Failed With Order Id : {@event.OrderId}, ErrorMessage: {@event.OrderId}");
        return Task.CompletedTask;
    }
}
