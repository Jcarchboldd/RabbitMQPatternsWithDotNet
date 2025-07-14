using MassTransit;
using Messaging.Common.Events;

namespace Consumer.API.A.Consumer.ConsumeMessage.Direct;

public class ConsumeDirectMessageDefinition : ConsumerDefinition<ConsumeDirectMessageHandler>
{
    protected override void ConfigureConsumer(
        IReceiveEndpointBuilder builder,
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<ConsumeDirectMessageHandler> consumerConfigurator)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rabbitConfigurator)
        {
            rabbitConfigurator.Bind<TestEvent>(x =>
            {
                x.ExchangeType = ExchangeType.Direct;
                x.RoutingKey = "consumer.api.a";
            });
        }

        if (builder is IRabbitMqBusFactoryConfigurator busConfigurator)
        {
            busConfigurator.Publish<TestEvent>(x => x.ExchangeType = ExchangeType.Direct);
        }
    }
}
