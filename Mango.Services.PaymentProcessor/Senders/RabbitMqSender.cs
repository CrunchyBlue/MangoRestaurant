using System.Text;
using Mango.MessageBus;
using Mango.Services.PaymentProcessor.Constants;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Mango.Services.PaymentProcessor.Senders;

/// <summary>
/// The rabbitmq sender.
/// </summary>
public class RabbitMqSender : IRabbitMqSender
{
    /// <summary>
    /// The connection.
    /// </summary>
    private readonly IConnection _connection;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="RabbitMqSender"/> class.
    /// </summary>
    public RabbitMqSender()
    {
        var factory = new ConnectionFactory()
        {
            HostName = SD.RabbitMqHostname,
            UserName = SD.RabbitMqUsername,
            Password = SD.RabbitMqPassword
        };

        _connection = factory.CreateConnection();
    }
    
    /// <inheritdoc cref="IRabbitMqSender.SendMessage"/>
    public void SendMessage(BaseMessage message, string exchangeName)
    {
        using var channel = _connection.CreateModel();
        channel.ExchangeDeclare(exchange: exchangeName, ExchangeType.Fanout, false, false, arguments: null);
        
        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        
        channel.BasicPublish(exchange: exchangeName, "", basicProperties: null, body: body);
    }
}