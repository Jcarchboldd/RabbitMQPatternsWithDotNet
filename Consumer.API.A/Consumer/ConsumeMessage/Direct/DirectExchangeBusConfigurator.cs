using MassTransit;
using Messaging.Common.Events;
using Messaging.Common.MassTransit;

namespace Consumer.API.A.Consumer.ConsumeMessage.Direct;

public class DirectExchangeBusConfigurator : IRabbitMqBusConfigurator
{
    public void Configure(IRabbitMqBusFactoryConfigurator configurator, IBusRegistrationContext context)
    {
        configurator.Publish<TestEvent>(x => x.ExchangeType = ExchangeType.Direct);
    }
}
