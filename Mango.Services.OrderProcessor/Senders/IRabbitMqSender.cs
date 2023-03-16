using Mango.MessageBus;

namespace Mango.Services.OrderProcessor.Senders;

/// <summary>
/// The rabbitmq sender interface.
/// </summary>
public interface IRabbitMqSender
{
    /// <summary>
    /// The send message.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="queueName">
    /// The queue name.
    /// </param>
    public void SendMessage(BaseMessage message, string queueName);
}