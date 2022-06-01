using EventBus.Base.Abstraction;
using Microsoft.Extensions.Logging;
using PaymentService.Api.IntegrationEvents.Events;

namespace NotificationService.IntegrationEvents.EventHandlers;

public class OrderPaymentSuccessIntegrationEventHandler : IIntegrationEventHandler<OrderPaymentSuccessIntegrationEvent>
{
    private readonly ILogger<OrderPaymentSuccessIntegrationEventHandler> logger;

    public OrderPaymentSuccessIntegrationEventHandler(ILogger<OrderPaymentSuccessIntegrationEventHandler> logger)
    {
        this.logger = logger;
    }

    public Task Handle(OrderPaymentSuccessIntegrationEvent @event)
    {
        //Send Success Notification (SMS, Email, Push)

        logger.LogInformation($" OrderPayment Success With Order Id : {@event.OrderId}");
        return Task.CompletedTask;
    }
}
