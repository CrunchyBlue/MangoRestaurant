using Mango.Services.OrderProcessor.Models;

namespace Mango.Services.OrderProcessor.Repository;

/// <summary>
/// The order repository interface.
/// </summary>
public interface IOrderRepository
{
    /// <summary>
    /// The add order.
    /// </summary>
    /// <param name="orderHeader">
    /// The order header.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{bool}"/>
    /// </returns>
    public Task<bool> AddOrder(OrderHeader orderHeader);

    /// <summary>
    /// The update order payment status.
    /// </summary>
    /// <param name="orderHeaderId">
    /// The order header id.
    /// </param>
    /// <param name="paid">
    /// Indicates whether an order is paid.
    /// </param>
    /// <returns></returns>
    public Task UpdateOrderPaymentStatus(int orderHeaderId, bool paid);
}