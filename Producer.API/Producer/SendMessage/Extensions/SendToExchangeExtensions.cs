using System.Text.Json;
using MassTransit;

namespace Producer.API.Producer.SendMessage.Extensions;

public static class SendToExchangeExtensions
{
    private static Uri BuildExchangeUri<T>(string exchangeType, Dictionary<string, object> parameters)
    {
        var query = parameters.Count > 0
            ? "&" + string.Join("&", parameters.Select(kvp => $"{kvp.Key}={kvp.Value}"))
            : string.Empty;
        return new Uri($"exchange:{typeof(T).Name}?type={exchangeType}{query}");
    }

    public static async Task SendToDirectExchange<T>(
        this ISendEndpointProvider provider,
        T message,
        string routingKey,
        ILogger logger,
        CancellationToken ct)
    {
        var uri = BuildExchangeUri<T>(
            "direct",
            new Dictionary<string, object> { ["routingKey"] = routingKey });
        await SendToUri(provider, message, uri, logger, ct);
    }

    public static async Task SendToTopicExchange<T>(
        this ISendEndpointProvider provider,
        T message,
        string routingKey,
        ILogger logger,
        CancellationToken ct)
    {
        var uri = BuildExchangeUri<T>(
            "topic",
            new Dictionary<string, object> { ["routingKey"] = routingKey });
        await SendToUri(provider, message, uri, logger, ct);
    }

    public static async Task SendToHeadersExchange<T>(
        this ISendEndpointProvider provider,
        T message,
        Dictionary<string, object> headers,
        ILogger logger,
        CancellationToken ct)
    {
        var uri = BuildExchangeUri<T>("headers", headers);
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