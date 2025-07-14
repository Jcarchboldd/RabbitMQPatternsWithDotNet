using System.Reflection;
using Carter;
using Messaging.Common.MassTransit;
using Producer.API.Producer.SendMessage.Fanout;
using Producer.API.Producer.SendMessage.Direct;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter();
builder.Services.AddOpenApi();
builder.Services.AddMessageBroker(builder.Configuration, Assembly.GetExecutingAssembly());

builder.Services.AddScoped<SendFanoutMessageHandler>();
builder.Services.AddScoped<SendDirectMessageHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapCarter();

app.Run();
