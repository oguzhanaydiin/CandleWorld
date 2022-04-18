using EventBus.Base;

namespace EventBus.UnitTests.Configs;

public class RabbitMqConfig : EventBusConfig
{
    public RabbitMqConfig()
    {
        ConnectionRetryCount = 5;
        SubscriberClientAppName = "EventBus.UnitTest";
        DefaultTopicName = "CandleWorldTopicName";
        EventBusType = EventBusType.RabbitMQ;
        EventNameSuffix = "IntegrationEvent";
    }
}
