using Mango.Services.Email.DbContexts;
using Mango.Services.Email.Messages;
using Mango.Services.Email.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.Email.Repository;

/// <summary>
/// The email repository.
/// </summary>
public class EmailRepository : IEmailRepository
{
    /// <summary>
    /// The db context.
    /// </summary>
    private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailRepository"/> class.
    /// </summary>
    /// <param name="dbContext"></param>
    public EmailRepository(DbContextOptions<ApplicationDbContext> dbContextOptions)
    {
        _dbContextOptions = dbContextOptions;
    }

    /// <inheritdoc cref="IEmailRepository.SendAndLogEmail"/>
    public async Task SendAndLogEmail(UpdatePaymentResultMessage message)
    {
        // TODO: Implement an email sender
        var emailLog = new EmailLog()
        {
            Email = message.Email,
            EmailSent = DateTime.Now,
            Log = $"Order - {message.OrderId} has been created successfully."
        };

        await using var dbContext = new ApplicationDbContext(_dbContextOptions);
        dbContext.EmailLogs.Add(emailLog);
        await dbContext.SaveChangesAsync();
    }
}