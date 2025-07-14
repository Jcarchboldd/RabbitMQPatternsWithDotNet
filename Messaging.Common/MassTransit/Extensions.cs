using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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

                foreach (var busConfigurator in context.GetServices<IRabbitMqBusConfigurator>())
                    busConfigurator.Configure(configurator, context);

                configure?.Invoke(configurator, context);

                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
