using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Messaging.Common.Events;

namespace Messaging.Common.MassTransit;

public static class MassTransitRegistrationExtensions
{
    public static IServiceCollection AddTestEventMessageBroker(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly assembly)
    {
        return services.AddDirectExchangeFor<TestEvent>(configuration, assembly);
    }
}
