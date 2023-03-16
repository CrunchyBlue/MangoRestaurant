namespace PaymentProcessor;

/// <summary>
/// The process payment interface.
/// </summary>
public interface IProcessPayment
{
    /// <summary>
    /// The payment processor
    /// </summary>
    /// <returns>
    /// The <see cref="T:bool"/>
    /// </returns>
    bool PaymentProcessor();
}