using Mango.MessageBus;
using Mango.Services.OrderProcessor.DbContexts;
using Mango.Services.OrderProcessor.Helpers;
using Mango.Services.OrderProcessor.Repository;
using Mango.Services.OrderProcessor.Constants;
using Mango.Services.OrderProcessor.Extensions;
using Mango.Services.OrderProcessor.Messaging;
using Mango.Services.OrderProcessor.Senders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
SD.CheckoutTopic = builder.Configuration["MessageBusTopics:CheckoutTopic"];
SD.CheckoutSubscription = builder.Configuration["MessageBusSubscriptions:CheckoutSubscription"];
SD.PaymentTopic = builder.Configuration["MessageBusTopics:PaymentTopic"];
SD.UpdateOrderTopic = builder.Configuration["MessageBusTopics:UpdateOrderTopic"];
SD.UpdateOrderSubscription = builder.Configuration["MessageBusSubscriptions:UpdateOrderSubscription"];

// Azure Service Bus
SD.ServiceBusConnectionString = builder.Configuration["ConnectionStrings:ServiceBus"];

// RabbitMQ
SD.RabbitMqHostname = builder.Configuration["RabbitMQ:Hostname"];
SD.RabbitMqUsername = builder.Configuration["RabbitMQ:Username"];
SD.RabbitMqPassword = builder.Configuration["RabbitMQ:Password"];

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var mapper = MappingConfig.RegisterMaps().CreateMapper();

var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddSingleton(mapper);
builder.Services.AddSingleton(new OrderRepository(optionsBuilder.Options));

// Azure Service Bus
builder.Services.AddSingleton<IServiceBusConsumer, ServiceBusConsumer>();
builder.Services.AddSingleton<IMessageBus, MessageBus>();

// TODO: Uncomment if desired to utilize RabbitMQ
// RabbitMQ
// builder.Services.AddSingleton<IRabbitMqSender, RabbitMqSender>();
// builder.Services.AddHostedService<RabbitMqCheckoutConsumer>();
// builder.Services.AddHostedService<RabbitMqUpdateOrderConsumer>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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