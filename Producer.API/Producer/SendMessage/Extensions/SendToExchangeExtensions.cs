using System.Text.Json;
using MassTransit;

namespace Producer.API.Producer.SendMessage.Extensions;

public static class SendToExchangeExtensions
{
    public static async Task SendToDirectExchange<T>(
        this ISendEndpointProvider provider,
        T message,
        string routingKey,
        ILogger logger,
        CancellationToken ct)
    {
        var uri = new Uri($"exchange:{typeof(T).Name}?type=direct&routingKey={routingKey}");
        await SendToUri(provider, message, uri, logger, ct);
    }

    public static async Task SendToTopicExchange<T>(
        this ISendEndpointProvider provider,
        T message,
        string routingKey,
        ILogger logger,
        CancellationToken ct)
    {
        var uri = new Uri($"exchange:{typeof(T).Name}?type=topic&routingKey={routingKey}");
        await SendToUri(provider, message, uri, logger, ct);
    }

    public static async Task SendToHeadersExchange<T>(
        this ISendEndpointProvider provider,
        T message,
        Dictionary<string, object> headers,
        ILogger logger,
        CancellationToken ct)
    {
        var args = string.Join("&", headers.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        var uri = new Uri($"exchange:{typeof(T).Name}?type=headers&{args}");
        await SendToUri(provider, message, uri, logger, ct);
    }

    private static async Task SendToUri<T>(
        ISendEndpointProvider provider,
        T message,
        Uri uri,
        ILogger logger,
        CancellationToken ct)
    {
        logger.LogInformation("Sending {EventType} to {Uri}: {Event}", typeof(T).Name, uri, JsonSerializer.Serialize(message));
        var endpoint = await provider.GetSendEndpoint(uri);
        await endpoint.Send(message ?? throw new ArgumentNullException(nameof(message)), ct);
    }
}