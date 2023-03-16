using Mango.Web.Models;

namespace Mango.Web.Services.IServices;

/// <summary>
/// The cart service interface.
/// </summary>
public interface ICartService : IBaseService
{
    /// <summary>
    /// The get cart by user id async.
    /// </summary>
    /// <typeparam name="T">
    /// The generic type.
    /// </typeparam>
    /// <param name="token">
    /// The token.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{T}"/>
    /// </returns>
    public Task<T> GetCartByUserIdAsync<T>(string userId, string token = null);
    
    /// <summary>
    /// The add to cart async.
    /// </summary>
    /// <typeparam name="T">
    /// The generic type.
    /// </typeparam>
    /// <param name="cartDto">
    /// The cart dto.
    /// </param>
    /// <param name="token">
    /// The token.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{T}"/>
    /// </returns>
    public Task<T> AddToCartAsync<T>(CartDto cartDto, string token);
    
    /// <summary>
    /// The update cart async.
    /// </summary>
    /// <typeparam name="T">
    /// The generic type.
    /// </typeparam>
    /// <param name="cartDto">
    /// The cart dto.
    /// </param>
    /// <param name="token">
    /// The token.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{T}"/>
    /// </returns>
    public Task<T> UpdateCartAsync<T>(CartDto cartDto, string token);
    
    /// <summary>
    /// The remove from cart async.
    /// </summary>
    /// <typeparam name="T">
    /// The generic type.
    /// </typeparam>
    /// <param name="cartDetailId">
    /// The cart detail id.
    /// </param>
    /// <param name="token">
    /// The token.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{T}"/>
    /// </returns>
    public Task<T> RemoveFromCartAsync<T>(int cartDetailId, string token);

    /// <summary>
    /// The clear cart async.
    /// </summary>
    /// <typeparam name="T">
    /// The generic type.
    /// </typeparam>
    /// <param name="userId">
    /// The user id.
    /// </param>
    /// <param name="token">
    /// The token.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{T}"/>
    /// </returns>
    public Task<T> ClearCartAsync<T>(string userId, string token);
    
    /// <summary>
    /// The apply coupon async.
    /// </summary>
    /// <typeparam name="T">
    /// The generic type.
    /// </typeparam>
    /// <param name="cartDto">
    /// The cart dto.
    /// </param>
    /// <param name="token">
    /// The token.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{T}"/>
    /// </returns>
    public Task<T> ApplyCouponAsync<T>(CartDto cartDto, string token);
    
    /// <summary>
    /// The remove coupon async.
    /// </summary>
    /// <typeparam name="T">
    /// The generic type.
    /// </typeparam>
    /// <param name="userId">
    /// The user id.
    /// </param>
    /// <param name="token">
    /// The token.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{T}"/>
    /// </returns>
    public Task<T> RemoveCouponAsync<T>(string userId, string token);

    /// <summary>
    /// The checkout async.
    /// </summary>
    /// <param name="cartHeader">
    /// The cart header.
    /// </param>
    /// <param name="token">
    /// The token.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{T}"/>
    /// </returns>
    public Task<T> CheckoutAsync<T>(CartHeaderDto cartHeader, string token);
}