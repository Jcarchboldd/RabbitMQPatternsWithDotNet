using MassTransit;
using Messaging.Common.Events;
using Messaging.Common.Constants;
using RabbitMQ.Client;

namespace Consumer.API.A.Consumer.ConsumeMessage.Direct;

public class ConsumeDirectMessageDefinition : ConsumerDefinition<ConsumeDirectMessageHandler>
{
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<ConsumeDirectMessageHandler> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.ConfigureConsumeTopology = false;

        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rabbitConfigurator)
        {
            rabbitConfigurator.Bind<TestEvent>(x =>
            {
                x.ExchangeType = ExchangeType.Direct;
                x.RoutingKey = RabbitMqRoutingKeys.ConsumerApiA;
            });
        }
    }
}
