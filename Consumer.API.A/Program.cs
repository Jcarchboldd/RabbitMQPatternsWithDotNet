using System.Reflection;
using Consumer.API.A.Consumer.ConsumeMessage.Fanout;
using Consumer.API.A.Consumer.ConsumeMessage.Direct;
using Messaging.Common.Events;
using RabbitMQ.Client;
using Messaging.Common.MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDirectExchangeFor<TestEvent>(
    builder.Configuration,
    Assembly.GetExecutingAssembly());
builder.Services.AddOpenApi();
builder.Services.AddScoped<ConsumeFanoutMessageHandler>();
builder.Services.AddScoped<ConsumeDirectMessageHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

