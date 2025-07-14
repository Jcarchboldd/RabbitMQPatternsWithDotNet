using System.Text.Json;
using MassTransit;

namespace Producer.API.Producer.SendMessage.Extensions;

public static class PublishExtensions
{
    public static async Task PublishWithLog<T>(
        this IPublishEndpoint publisher,
        T message,
        ILogger logger,
        CancellationToken ct)
    {
        logger.LogInformation("Publishing {EventType}: {Event}", typeof(T).Name, JsonSerializer.Serialize(message));
        await publisher.Publish(message ?? throw new ArgumentNullException(nameof(message)), ct);
    }
    
}