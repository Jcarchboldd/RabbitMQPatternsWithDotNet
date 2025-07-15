using MassTransit;
using Messaging.Common.Events;
using Producer.API.Producer.SendMessage.Extensions;
using Messaging.Common.Constants;

namespace Producer.API.Producer.SendMessage.Direct;

public class SendDirectMessageHandler(ISendEndpointProvider provider, ILogger<SendDirectMessageHandler> logger)
{
    private const string RoutingKey = RabbitMqRoutingKeys.ConsumerApiA;

    public async Task HandleAsync(TestEvent message, CancellationToken ct)
    {
        await provider.SendToDirectExchange(message, RoutingKey, logger, ct);
    }
}
