using Carter;
using Messaging.Common.Events;

namespace Producer.API.Producer.SendMessage.Direct;

public class SendDirectMessageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/send/direct", async (
                TestEvent request,
                SendDirectMessageHandler handler,
                CancellationToken ct) =>
            {
                await handler.HandleAsync(request, ct);
                return Results.Accepted();
            })
            .WithName("SendDirectMessage")
            .Produces(200)
            .ProducesProblem(400)
            .WithSummary("Send Direct Message")
            .WithDescription("Send Direct Message");
    }
}
