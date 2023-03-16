using System.Text;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;

namespace Mango.MessageBus;

/// <summary>
/// The message bus.
/// </summary>
public class MessageBus : IMessageBus
{
    /// <inheritdoc cref="IMessageBus.PublishMessage"/>
    public async Task PublishMessage(BaseMessage message, string topic, string connectionString)
    {
        await using var client = new ServiceBusClient(connectionString);
        await using var sender = client.CreateSender(topic);

        var jsonMessage = JsonConvert.SerializeObject(message);
        var finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
        {
            CorrelationId = Guid.NewGuid().ToString()
        };

        await sender.SendMessageAsync(finalMessage);
    }
}