using System.Text;
using Mango.Services.PaymentProcessor.Constants;
using Mango.Services.PaymentProcessor.Messages;
using Mango.Services.PaymentProcessor.Senders;
using Newtonsoft.Json;
using PaymentProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Mango.Services.PaymentProcessor.Messaging;

/// <summary>
/// The rabbitmq consumer.
/// </summary>
public class RabbitMqConsumer : BackgroundService
{
    /// <summary>
    /// The process payment.
    /// </summary>
    private readonly IProcessPayment _processPayment;
    
    /// <summary>
    /// The connection.
    /// </summary>
    private IConnection _connection;

    /// <summary>
    /// The channel.
    /// </summary>
    private readonly IModel _channel;
    
    /// <summary>
    /// The rabbitmq sender.
    /// </summary>
    private readonly IRabbitMqSender _rabbitMqSender;

    /// <summary>
    /// Initializes a new instance of the <see cref="RabbitMqConsumer"/> class.
    /// </summary>
    /// <param name="processPayment">
    /// The process payment.
    /// </param>
    /// <param name="rabbitMqSender">
    /// The rabbitmq sender.
    /// </param>
    public RabbitMqConsumer(IProcessPayment processPayment, IRabbitMqSender rabbitMqSender)
    {
        _processPayment = processPayment;
        _rabbitMqSender = rabbitMqSender;
        
        var factory = new ConnectionFactory()
        {
            HostName = SD.RabbitMqHostname,
            UserName = SD.RabbitMqUsername,
            Password = SD.RabbitMqPassword
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: SD.PaymentSubscription, false, false, false, arguments: null);
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

            var paymentRequestMessage = JsonConvert.DeserializeObject<PaymentRequestMessage>(body);

            var result = _processPayment.PaymentProcessor();

            if (paymentRequestMessage != null)
            {
                var updatePaymentResultMessage = new UpdatePaymentResultMessage()
                {
                    Status = result,
                    OrderId = paymentRequestMessage.OrderId,
                    Email = paymentRequestMessage.Email
                };

                try
                {
                    _rabbitMqSender.SendMessage(updatePaymentResultMessage, SD.UpdateOrderTopic);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume(SD.PaymentSubscription, false, consumer);
    }
}