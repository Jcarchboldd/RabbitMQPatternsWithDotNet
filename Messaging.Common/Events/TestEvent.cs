namespace Messaging.Common.Events;

public record TestEvent : IntegrationEvent
{
    public string UserName { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
};