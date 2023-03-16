using Mango.Services.Email.Messages;

namespace Mango.Services.Email.Repository;

/// <summary>
/// The email repository interface.
/// </summary>
public interface IEmailRepository
{
    /// <summary>
    /// The send and log email.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task"/>
    /// </returns>
    Task SendAndLogEmail(UpdatePaymentResultMessage message);
}