using System.Text;
using Azure.Messaging.ServiceBus;
using Mango.MessageBus;
using Mango.Services.PaymentProcessor.Constants;
using Mango.Services.PaymentProcessor.Messages;
using Newtonsoft.Json;
using PaymentProcessor;

namespace Mango.Services.PaymentProcessor.Messaging;

/// <summary>
/// The service bus consumer.
/// </summary>
public class ServiceBusConsumer : IServiceBusConsumer
{
    /// <summary>
    /// The process payment.
    /// </summary>
    private readonly IProcessPayment _processPayment;

    /// <summary>
    /// The payment processor.
    /// </summary>
    private readonly ServiceBusProcessor _paymentProcessor;

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
    public ServiceBusConsumer(IProcessPayment processPayment, IMessageBus messageBus)
    {
        _processPayment = processPayment;
        _messageBus = messageBus;

        var client = new ServiceBusClient(SD.ServiceBusConnectionString);

        _paymentProcessor = client.CreateProcessor(SD.PaymentTopic, SD.PaymentSubscription);
    }

    /// <summary>
    /// The start.
    /// </summary>
    public async Task Start()
    {
        _paymentProcessor.ProcessMessageAsync += OnPaymentMessageReceived;
        _paymentProcessor.ProcessErrorAsync += HandleError;
        await _paymentProcessor.StartProcessingAsync();
    }

    /// <summary>
    /// The stop.
    /// </summary>
    public async Task Stop()
    {
        await _paymentProcessor.StopProcessingAsync();
        await _paymentProcessor.DisposeAsync();
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
    /// The on payment message received.
    /// </summary>
    /// <param name="args">
    /// The args.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task"/>
    /// </returns>
    private async Task OnPaymentMessageReceived(ProcessMessageEventArgs args)
    {
        var message = args.Message;
        var body = Encoding.UTF8.GetString(message.Body);
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
                await _messageBus.PublishMessage(updatePaymentResultMessage, SD.UpdateOrderTopic, SD.ServiceBusConnectionString);
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