using Mango.Services.ShoppingCartAPI.Models.Dtos;

namespace Mango.Services.ShoppingCartAPI.Repository;

/// <summary>
/// The cart repository interface.
/// </summary>
public interface ICartRepository
{
    /// <summary>
    /// The get cart by user id.
    /// </summary>
    /// <param name="userId">
    /// The user id.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{CartDto}"/>
    /// </returns>
    public Task<CartDto> GetCartByUserId(string userId);

    /// <summary>
    /// The create update cart.
    /// </summary>
    /// <param name="cartDto">
    /// The cart dto.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{CartDto}"/>
    /// </returns>
    public Task<CartDto> CreateUpdateCart(CartDto cartDto);

    /// <summary>
    /// The remove from cart.
    /// </summary>
    /// <param name="cartDetailId">
    /// The cart detail id.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{bool}"/>
    /// </returns>
    public Task<bool> RemoveFromCart(int cartDetailId);
    
    /// <summary>
    /// The clear cart.
    /// </summary>
    /// <param name="userId">
    /// The user id.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{bool}"/>
    /// </returns>
    public Task<bool> ClearCart(string userId);

    /// <summary>
    /// The apply coupon.
    /// </summary>
    /// <param name="userId">
    /// The user id.
    /// </param>
    /// <param name="couponCode">
    /// The coupon code.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{bool}"/>
    /// </returns>
    public Task<bool> ApplyCoupon(string userId, string couponCode);
    
    /// <summary>
    /// The remove coupon.
    /// </summary>
    /// <param name="userId">
    /// The user id.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{bool}"/>
    /// </returns>
    public Task<bool> RemoveCoupon(string userId);
}