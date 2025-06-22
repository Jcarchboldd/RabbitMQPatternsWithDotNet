namespace Messaging.Common.Events;

public record IntegrationEvent
{
    public Guid Id =>  Guid.NewGuid();
    public DateTime OccuredOn => DateTime.UtcNow;
    public string? EventName => GetType().AssemblyQualifiedName;
};