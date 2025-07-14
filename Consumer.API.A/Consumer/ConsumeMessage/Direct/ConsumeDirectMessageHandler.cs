using System.Text.Json;
using MassTransit;
using Messaging.Common.Events;

namespace Consumer.API.A.Consumer.ConsumeMessage.Direct;

public class ConsumeDirectMessageHandler(ILogger<ConsumeDirectMessageHandler> logger) : IConsumer<TestEvent>
{
    public Task Consume(ConsumeContext<TestEvent> context)
    {
        logger.LogInformation("Direct TestEvent handled: {Event}", JsonSerializer.Serialize(context.Message));
        return Task.CompletedTask;
    }
}
