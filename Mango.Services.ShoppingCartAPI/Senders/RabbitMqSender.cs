using System.Text;
using Mango.MessageBus;
using Mango.Services.ShoppingCartAPI.Constants;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Mango.Services.ShoppingCartAPI.Senders;

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
    public void SendMessage(BaseMessage message, string queueName)
    {
        using var channel = _connection.CreateModel();
        channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);
        
        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        
        channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
    }
}