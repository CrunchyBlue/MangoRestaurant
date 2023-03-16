using System.Text;
using Azure.Messaging.ServiceBus;
using Mango.Services.Email.Constants;
using Mango.Services.Email.Messages;
using Mango.Services.Email.Repository;
using Newtonsoft.Json;

namespace Mango.Services.Email.Messaging;

/// <summary>
/// The service bus consumer.
/// </summary>
public class ServiceBusConsumer : IServiceBusConsumer
{
    /// <summary>
    /// The email repository.
    /// </summary>
    private readonly EmailRepository _emailRepository;

    /// <summary>
    /// The update order processor.
    /// </summary>
    private readonly ServiceBusProcessor _updateOrderProcessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceBusConsumer"/> class.
    /// </summary>
    /// <param name="orderRepository">
    /// The order repository.
    /// </param>
    /// <param name="mapper">
    /// The mapper.
    /// </param>
    public ServiceBusConsumer(EmailRepository emailRepository)
    {
        _emailRepository = emailRepository;

        var client = new ServiceBusClient(SD.ServiceBusConnectionString);
        
        _updateOrderProcessor = client.CreateProcessor(SD.UpdateOrderTopic, SD.EmailSubscription);
    }

    /// <summary>
    /// The start.
    /// </summary>
    public async Task Start()
    {
        _updateOrderProcessor.ProcessMessageAsync += OnUpdateOrderMessageReceived;
        _updateOrderProcessor.ProcessErrorAsync += HandleError;
        await _updateOrderProcessor.StartProcessingAsync();
    }
    
    /// <summary>
    /// The stop.
    /// </summary>
    public async Task Stop()
    {
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
                await _emailRepository.SendAndLogEmail(paymentResultMessage);
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