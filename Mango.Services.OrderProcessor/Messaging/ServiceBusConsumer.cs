using System.Text;
using AutoMapper;
using Azure.Messaging.ServiceBus;
using Mango.MessageBus;
using Mango.Services.OrderProcessor.Constants;
using Mango.Services.OrderProcessor.Messages;
using Mango.Services.OrderProcessor.Models;
using Mango.Services.OrderProcessor.Repository;
using Newtonsoft.Json;

namespace Mango.Services.OrderProcessor.Messaging;

/// <summary>
/// The service bus consumer.
/// </summary>
public class ServiceBusConsumer : IServiceBusConsumer
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
    /// The checkout processor.
    /// </summary>
    private readonly ServiceBusProcessor _checkoutProcessor;
    
    /// <summary>
    /// The update order processor.
    /// </summary>
    private readonly ServiceBusProcessor _updateOrderProcessor;

    /// <summary>
    /// The message bus.
    /// </summary>
    private readonly IMessageBus _messageBus;

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceBusConsumer"/> class.
    /// </summary>
    /// <param name="orderRepository">
    /// The order repository.
    /// </param>
    /// <param name="mapper">
    /// The mapper.
    /// </param>
    public ServiceBusConsumer(OrderRepository orderRepository, IMapper mapper, IMessageBus messageBus)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _messageBus = messageBus;

        var client = new ServiceBusClient(SD.ServiceBusConnectionString);

        _checkoutProcessor = client.CreateProcessor(SD.CheckoutTopic, SD.CheckoutSubscription);
        _updateOrderProcessor = client.CreateProcessor(SD.UpdateOrderTopic, SD.UpdateOrderSubscription);
    }

    /// <summary>
    /// The start.
    /// </summary>
    public async Task Start()
    {
        _checkoutProcessor.ProcessMessageAsync += OnCheckoutMessageReceived;
        _checkoutProcessor.ProcessErrorAsync += HandleError;
        await _checkoutProcessor.StartProcessingAsync();
        
        _updateOrderProcessor.ProcessMessageAsync += OnUpdateOrderMessageReceived;
        _updateOrderProcessor.ProcessErrorAsync += HandleError;
        await _updateOrderProcessor.StartProcessingAsync();
    }
    
    /// <summary>
    /// The stop.
    /// </summary>
    public async Task Stop()
    {
        await _checkoutProcessor.StopProcessingAsync();
        await _checkoutProcessor.DisposeAsync();
        
        await _updateOrderProcessor.StopProcessingAsync();
        await _updateOrderProcessor.DisposeAsync();
    }

    /// <summary>
    /// The handle error.
    /// </summary>
    /// <param name="args">
    /// The args.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task"/>
    /// </returns>
    private static Task HandleError(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }
    
    /// <summary>
    /// The on checkout message received.
    /// </summary>
    /// <param name="args">
    /// The args.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task"/>
    /// </returns>
    private async Task OnCheckoutMessageReceived(ProcessMessageEventArgs args)
    {
        var message = args.Message;
        var body = Encoding.UTF8.GetString(message.Body);
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
            await _messageBus.PublishMessage(paymentRequestMessage, SD.PaymentTopic, SD.ServiceBusConnectionString);
            await args.CompleteMessageAsync(args.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    /// <summary>
    /// The on update order message received.
    /// </summary>
    /// <param name="args">
    /// The args.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task"/>
    /// </returns>
    private async Task OnUpdateOrderMessageReceived(ProcessMessageEventArgs args)
    {
        var message = args.Message;
        var body = Encoding.UTF8.GetString(message.Body);
        var paymentResultMessage = JsonConvert.DeserializeObject<UpdatePaymentResultMessage>(body);

        if (paymentResultMessage != null)
        {
            try
            {
                await _orderRepository.UpdateOrderPaymentStatus(paymentResultMessage.OrderId, paymentResultMessage.Status);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}