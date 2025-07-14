namespace Messaging.Common.Events;

public record IntegrationEvent
{
    /// <summary>
    /// Unique identifier for the event.
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Time when the event occurred.
    /// </summary>
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;

    public string? EventName => GetType().AssemblyQualifiedName;
}