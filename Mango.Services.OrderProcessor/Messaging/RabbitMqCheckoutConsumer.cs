using System.Text;
using AutoMapper;
using Mango.Services.OrderProcessor.Constants;
using Mango.Services.OrderProcessor.Messages;
using Mango.Services.OrderProcessor.Models;
using Mango.Services.OrderProcessor.Repository;
using Mango.Services.OrderProcessor.Senders;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Mango.Services.OrderProcessor.Messaging;

/// <summary>
/// The rabbitmq checkout consumer.
/// </summary>
public class RabbitMqCheckoutConsumer : BackgroundService
{
    /// <summary>
    /// The order repository.
    /// </summary>
    private readonly OrderRepository _orderRepository;

    /// <summary>
    /// The mapper.
    /// </summary>
    private readonly IMapper _mapper;

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
    private readonly IRabbitMqSender _rabbitMQSender;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="RabbitMqCheckoutConsumer"/> class.
    /// </summary>
    /// <param name="orderRepository">
    /// The order repository.
    /// </param>
    /// <param name="mapper">
    /// The mapper.
    /// </param>
    /// <param name="rabbitMQSender">
    /// The rabbitmq sender
    /// </param>
    public RabbitMqCheckoutConsumer(OrderRepository orderRepository, IMapper mapper, IRabbitMqSender rabbitMQSender)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _rabbitMQSender = rabbitMQSender;

        var factory = new ConnectionFactory()
        {
            HostName = SD.RabbitMqHostname,
            UserName = SD.RabbitMqUsername,
            Password = SD.RabbitMqPassword
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: SD.CheckoutSubscription, false, false, false, arguments: null);
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

            var checkoutHeaderDto = JsonConvert.DeserializeObject<CheckoutHeaderDto>(body);

            var orderHeader = _mapper.Map<OrderHeader>(checkoutHeaderDto);

            await _orderRepository.AddOrder(orderHeader);

            var paymentRequestMessage = new PaymentRequestMessage()
            {
                Name = $"{orderHeader.FirstName} {orderHeader.LastName}",
                CardNumber = orderHeader.CardNumber,
                CVV = orderHeader.CVV,
                ExpiryMonthYear = orderHeader.ExpiryMonthYear,
                OrderId = orderHeader.OrderHeaderId,
                OrderTotal = orderHeader.OrderTotal,
                Email = orderHeader.Email
            };

            try
            {
                _rabbitMQSender.SendMessage(paymentRequestMessage, SD.PaymentTopic);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume(SD.CheckoutSubscription, false, consumer);
    }
}