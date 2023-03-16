using System.Text;
using Mango.Services.Email.Constants;
using Mango.Services.Email.Messages;
using Mango.Services.Email.Repository;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Mango.Services.Email.Messaging;

/// <summary>
/// The rabbitmq consumer.
/// </summary>
public class RabbitMqConsumer : BackgroundService
{
    /// <summary>
    /// The email repository.
    /// </summary>
    private readonly EmailRepository _emailRepository;
    
    /// <summary>
    /// The connection.
    /// </summary>
    private IConnection _connection;

    /// <summary>
    /// The channel.
    /// </summary>
    private readonly IModel _channel;

    /// <summary>
    /// The queue name.
    /// </summary>
    private readonly string _queueName;

    /// <summary>
    /// Initializes a new instance of the <see cref="RabbitMqConsumer"/> class.
    /// </summary>
    /// <param name="emailRepository">
    /// The email repository.
    /// </param>
    public RabbitMqConsumer(EmailRepository emailRepository)
    {
        _emailRepository = emailRepository;
        
        var factory = new ConnectionFactory()
        {
            HostName = SD.RabbitMqHostname,
            UserName = SD.RabbitMqUsername,
            Password = SD.RabbitMqPassword
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(exchange: SD.UpdateOrderTopic, ExchangeType.Fanout, false, false, arguments: null);
        _queueName = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(_queueName, SD.UpdateOrderTopic, "");
    }
    
    /// <summary>
    /// The execute async.
    /// </summary>
    /// <param name="stoppingToken">
    /// The stopping token.
    /// </param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (ch, ea) =>
        {
            var body = Encoding.UTF8.GetString(ea.Body.ToArray());

            var updatePaymentResultMessage = JsonConvert.DeserializeObject<UpdatePaymentResultMessage>(body);

            if (updatePaymentResultMessage != null)
            {
                try
                {
                    await _emailRepository.SendAndLogEmail(updatePaymentResultMessage);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume(_queueName, false, consumer);
    }
}