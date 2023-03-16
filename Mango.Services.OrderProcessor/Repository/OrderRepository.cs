using Mango.Services.OrderProcessor.DbContexts;
using Mango.Services.OrderProcessor.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.OrderProcessor.Repository;

/// <summary>
/// The order repository.
/// </summary>
public class OrderRepository : IOrderRepository
{
    /// <summary>
    /// The db context.
    /// </summary>
    private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderRepository"/> class.
    /// </summary>
    /// <param name="dbContext"></param>
    public OrderRepository(DbContextOptions<ApplicationDbContext> dbContextOptions)
    {
        _dbContextOptions = dbContextOptions;
    }
    
    /// <inheritdoc cref="IOrderRepository.AddOrder"/>
    public async Task<bool> AddOrder(OrderHeader orderHeader)
    {
        try
        {
            await using var dbContext = new ApplicationDbContext(_dbContextOptions);
            dbContext.OrderHeaders.Add(orderHeader);
            await dbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    /// <inheritdoc cref="IOrderRepository.UpdateOrderPaymentStatus"/>
    public async Task UpdateOrderPaymentStatus(int orderHeaderId, bool paid)
    {
        try
        {
            await using var dbContext = new ApplicationDbContext(_dbContextOptions);
            var orderHeader = await dbContext.OrderHeaders.FirstOrDefaultAsync(oh => oh.OrderHeaderId == orderHeaderId);

            if (orderHeader != null)
            {
                orderHeader.PaymentStatus = paid;
                await dbContext.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);
        }
    }
}