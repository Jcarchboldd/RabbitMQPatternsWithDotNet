using System.Text.Json;
using MassTransit;
using Messaging.Common.Events;

namespace Producer.API.Producer.SendMessage.Fanout;

public class SendFanoutMessageHandler(IPublishEndpoint publisher, ILogger<SendFanoutMessageHandler> logger)
{
    public async Task HandleAsync(TestEvent testEvent, CancellationToken ct)
    {
        logger.LogInformation("Publishing Fanout TestEvent: {Event}", JsonSerializer.Serialize(testEvent));
        await publisher.Publish(testEvent, ct);
    }
}