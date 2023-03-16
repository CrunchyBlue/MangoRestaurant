namespace Mango.MessageBus;

/// <summary>
/// The message bus interface.
/// </summary>
public interface IMessageBus
{
    /// <summary>
    /// The publish message.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="topic">
    /// The topic.
    /// </param>
    /// <param name="connectionString">
    /// The connection string.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task"/>
    /// </returns>
    public Task PublishMessage(BaseMessage message, string topic, string connectionString);
}