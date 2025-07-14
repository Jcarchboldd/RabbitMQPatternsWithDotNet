using Carter;
using Messaging.Common.Events;

namespace Producer.API.Producer.SendMessage.Fanout;

public class SendFanoutMessageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/send/fanout", async (
                TestEvent request,
                SendFanoutMessageHandler handler,
                CancellationToken ct) =>
            {
                await handler.HandleAsync(request, ct);
                return Results.Accepted();
            })
            .WithName("SendFanoutMessage")
            .Produces(200)
            .ProducesProblem(400)
            .WithSummary("Send Fanout Message")
            .WithDescription("Send Fanout Message");
    }
}