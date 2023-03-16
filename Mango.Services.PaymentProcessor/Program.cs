using Mango.MessageBus;
using Mango.Services.PaymentProcessor.Constants;
using Mango.Services.PaymentProcessor.Extensions;
using Mango.Services.PaymentProcessor.Messaging;
using Mango.Services.PaymentProcessor.Senders;
using PaymentProcessor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
SD.PaymentTopic = builder.Configuration["MessageBusTopics:PaymentTopic"];
SD.PaymentSubscription = builder.Configuration["MessageBusSubscriptions:PaymentSubscription"];
SD.UpdateOrderTopic = builder.Configuration["MessageBusTopics:UpdateOrderTopic"];

// Azure Service Bus
SD.ServiceBusConnectionString = builder.Configuration["ConnectionStrings:ServiceBus"];

// RabbitMQ
SD.RabbitMqHostname = builder.Configuration["RabbitMQ:Hostname"];
SD.RabbitMqUsername = builder.Configuration["RabbitMQ:Username"];
SD.RabbitMqPassword = builder.Configuration["RabbitMQ:Password"];

builder.Services.AddSingleton<IProcessPayment, ProcessPayment>();

// Azure Service Bus
builder.Services.AddSingleton<IServiceBusConsumer, ServiceBusConsumer>();
builder.Services.AddSingleton<IMessageBus, MessageBus>();

// TODO: Uncomment if desired to utilize RabbitMQ
// RabbitMQ
// builder.Services.AddSingleton<IRabbitMqSender, RabbitMqSender>();
// builder.Services.AddHostedService<RabbitMqConsumer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseServiceBusConsumer();

app.Run();