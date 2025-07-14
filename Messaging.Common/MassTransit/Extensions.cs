using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Messaging.Common.MassTransit;

public static class Extensions
{
    public static IServiceCollection AddMessageBroker(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly? assembly = null,
        Action<IRabbitMqBusFactoryConfigurator, IBusRegistrationContext>? configure = null)
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            if (assembly != null)
                config.AddConsumers(assembly);

            config.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(configuration["MessageBroker:Host"]!), host =>
                {
                    host.Username(configuration["MessageBroker:UserName"] ?? string.Empty);
                    host.Password(configuration["MessageBroker:Password"] ?? string.Empty);
                });

                configure?.Invoke(configurator, context);

                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
