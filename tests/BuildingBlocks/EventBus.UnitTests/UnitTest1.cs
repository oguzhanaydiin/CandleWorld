using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using EventBus.UnitTest.Events.EventHandlers;
using EventBus.UnitTest.Events.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using RabbitMQ.Client;

namespace EventBus.UnitTests
{
    public class Tests
    {
        private ServiceCollection services;

        [SetUp]
        public void Setup()
        {
            services = new ServiceCollection();
            services.AddLogging(configure => configure.AddConsole());
        }

        [Test]
        public void Subscribe_event_on_rabbitmq_test()
        {
            services.AddSingleton<IEventBus>(sp =>
            {
                EventBusConfig config = new()
                {
                    ConnectionRetryCount = 5,
                    SubscriberClientAppName = "EventBus.UnitTest",
                    DefaultTopicName = "CandleWorldTopicName",
                    EventBusType = EventBusType.RabbitMQ,
                    EventNameSuffix = "IntegrationEvent",
                    /*Connection = new ConnectionFactory()
                    {
                        HostName = "localhost",
                        Port = 5672,
                        UserName = "guest",
                        Password = "guest"

                    }*/
                };

                return EventBusFactory.Create(config, sp);
            });
            var serviceProvider = services.BuildServiceProvider();

            var eventBus = serviceProvider.GetRequiredService<IEventBus>(); //Burada EventBus istenildiğinde 24. satırda başlayan alan çalışacak
                                                                            //Bize verdiğimiz confige uygun bir event bus verecek (rabbitmq veya azure)

            eventBus.Subscribe<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();
            eventBus.UnSubscribe<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();

            //rabbitmq-plugins enable rabbitmq-management
            Assert.Pass();
        }
    }
}