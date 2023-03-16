namespace Mango.MessageBus;

/// <summary>
/// The base message.
/// </summary>
public class BaseMessage
{
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Gets or sets the message created.
    /// </summary>
    public DateTime MessageCreated { get; set; }
}