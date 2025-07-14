using MassTransit;
using Messaging.Common.Events;

namespace Consumer.API.A.Consumer.ConsumeMessage.Direct;

public class ConsumeDirectMessageDefinition : ConsumerDefinition<ConsumeDirectMessageHandler>
{
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<ConsumeDirectMessageHandler> consumerConfigurator)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;
        endpointConfigurator.Bind<TestEvent>(x =>
        {
            x.ExchangeType = ExchangeType.Direct;
            x.RoutingKey = "consumer.api.a";
        });
    }
}
