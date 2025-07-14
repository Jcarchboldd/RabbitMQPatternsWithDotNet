namespace Messaging.Common.MassTransit;

using MassTransit;

public interface IRabbitMqBusConfigurator
{
    void Configure(IRabbitMqBusFactoryConfigurator configurator, IBusRegistrationContext context);
}
