using Mango.Services.Email.DbContexts;
using Mango.Services.Email.Repository;
using Mango.Services.Email.Constants;
using Mango.Services.Email.Extensions;
using Mango.Services.Email.Messaging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
SD.UpdateOrderTopic = builder.Configuration["MessageBusTopics:UpdateOrderTopic"];
SD.EmailSubscription = builder.Configuration["MessageBusSubscriptions:EmailSubscription"];

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

var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddSingleton(new EmailRepository(optionsBuilder.Options));

// Azure Service Bus
builder.Services.AddSingleton<IServiceBusConsumer, ServiceBusConsumer>();

// TODO: Uncomment if desired to utilize RabbitMQ
// RabbitMQ
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