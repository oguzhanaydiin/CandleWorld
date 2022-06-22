using OrderService.Api.IntegrationEvents.EventHandlers;

namespace OrderService.Api.Extensions.Registiration.EventHandleRegistiration;

public static class EventHandlerRegistiration
{
    public static IServiceCollection ConfigureEventHandlers(this IServiceCollection services)
    {
        services.AddTransient<OrderCreatedIntegrationEventHandler>();
        return services;
    }
}
