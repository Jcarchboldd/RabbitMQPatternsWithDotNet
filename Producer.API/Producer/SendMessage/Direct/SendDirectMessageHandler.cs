using MassTransit;
using Messaging.Common.Events;
using Producer.API.Producer.SendMessage.Extensions;

namespace Producer.API.Producer.SendMessage.Direct;

public class SendDirectMessageHandler(ISendEndpointProvider provider, ILogger<SendDirectMessageHandler> logger)
{
    private const string RoutingKey = "consumer.api.a";

    public async Task HandleAsync(TestEvent message, CancellationToken ct)
    {
        await provider.SendToDirectExchange(message, RoutingKey, logger, ct);
    }
}
