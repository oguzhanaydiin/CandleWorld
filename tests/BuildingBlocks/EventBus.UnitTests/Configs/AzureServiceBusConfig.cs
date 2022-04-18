using EventBus.Base;

namespace EventBus.UnitTests.Configs;

public class AzureServiceBusConfig : EventBusConfig
{
    public AzureServiceBusConfig()
    {
        ConnectionRetryCount = 5;
        SubscriberClientAppName = "EventBus.UnitTest";
        DefaultTopicName = "CandleWorldTopicName";
        EventBusType = EventBusType.AzureServiceBus;
        EventNameSuffix = "IntegrationEvent";
        EventBusConnectionString = "Endpoint=sb://candleworld.servicebus.windows.net/;SharedAccessKeyName=NewPolicy;SharedAccessKey=7sJghGWFOXaUaRblrbzOIIf4bQk6qkbTN/SEnKjXLpE=";
    }
}
