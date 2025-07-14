using MassTransit;
using Messaging.Common.Events;

namespace Consumer.API.A.Consumer.ConsumeMessage.Fanout;

public class ConsumeFanoutMessageHandler(ILogger<ConsumeFanoutMessageHandler> logger) : IConsumer<TestEvent>
{
    public Task Consume(ConsumeContext<TestEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        return Task.CompletedTask;
    }
}