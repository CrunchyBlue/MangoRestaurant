namespace Mango.Services.PaymentProcessor.Messaging;

/// <summary>
/// The service bus consumer interface.
/// </summary>
public interface IServiceBusConsumer
{
    /// <summary>
    /// The start.
    /// </summary>
    /// <returns>
    /// The <see cref="T:Task"/>
    /// </returns>
    public Task Start();

    /// <summary>
    /// The stop.
    /// </summary>
    /// <returns>
    /// The <see cref="T:Task"/>
    /// </returns>
    public Task Stop();
}