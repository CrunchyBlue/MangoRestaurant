namespace Mango.Services.OrderProcessor.Messages;

/// <summary>
/// The update payment result message.
/// </summary>
public class UpdatePaymentResultMessage
{
    /// <summary>
    /// Gets or sets the order id.
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    public bool Status { get; set; }
    
    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    public string Email { get; set; }
}