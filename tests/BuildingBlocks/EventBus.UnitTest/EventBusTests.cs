using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using EventBus.Base;
using RabbitMQ.Client;
using EventBus.UnitTest.Events.Events;
using EventBus.UnitTest.Events.EventHandlers;

namespace EventBus.UnitTest
{
    [TestClass]
    public class EventBusTests
    {
        private ServiceCollection services;
        public EventBusTests()
        {
            services = new ServiceCollection();
            services.AddLogging(configure => configure.AddConsole());
        }

        [TestMethod]
        public void subscribe_event_on_rabbitmq_test()
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
                   //Bunlar zaten default ayarlar vermesek de bunlarla bağlanacak
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

        }

    }
}