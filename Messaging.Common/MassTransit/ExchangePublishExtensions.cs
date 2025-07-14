using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Messaging.Common.MassTransit;

public static class ExchangePublishExtensions
{
    public static IServiceCollection AddMessageBrokerWithExchange<T>(
        this IServiceCollection services,
        IConfiguration configuration,
        string exchangeType,
        Assembly? assembly = null)
        where T : class
    {
        return services.AddMessageBroker(
            configuration,
            assembly,
            (configurator, _) => configurator.Publish<T>(x => x.ExchangeType = exchangeType));
    }

    public static IServiceCollection AddDirectExchangeFor<T>(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly? assembly = null)
        where T : class
    {
        return services.AddMessageBrokerWithExchange<T>(configuration, ExchangeType.Direct, assembly);
    }

    public static IServiceCollection AddFanoutExchangeFor<T>(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly? assembly = null)
        where T : class
    {
        return services.AddMessageBrokerWithExchange<T>(configuration, ExchangeType.Fanout, assembly);
    }

    public static IServiceCollection AddTopicExchangeFor<T>(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly? assembly = null)
        where T : class
    {
        return services.AddMessageBrokerWithExchange<T>(configuration, ExchangeType.Topic, assembly);
    }

    public static IServiceCollection AddHeadersExchangeFor<T>(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly? assembly = null)
        where T : class
    {
        return services.AddMessageBrokerWithExchange<T>(configuration, ExchangeType.Headers, assembly);
    }
}
