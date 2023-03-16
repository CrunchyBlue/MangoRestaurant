using Mango.MessageBus;

namespace Mango.Services.OrderProcessor.Messages;

/// <summary>
/// The payment request message.
/// </summary>
public class PaymentRequestMessage : BaseMessage
{
    /// <summary>
    /// Gets or sets the order id.
    /// </summary>
    public int OrderId { get; set; }
    
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Gets or sets the card number.
    /// </summary>
    public string CardNumber { get; set; }
    
    /// <summary>
    /// Gets or sets the cvv.
    /// </summary>
    public string CVV { get; set; }
    
    /// <summary>
    /// Gets or sets the expiry month year.
    /// </summary>
    public string ExpiryMonthYear { get; set; }
    
    /// <summary>
    /// Gets or sets the order total.
    /// </summary>
    public double OrderTotal { get; set; }

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    public string Email { get; set; }
}