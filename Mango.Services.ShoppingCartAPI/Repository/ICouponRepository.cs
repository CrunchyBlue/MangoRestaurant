using Mango.Services.ShoppingCartAPI.Models.Dtos;

namespace Mango.Services.ShoppingCartAPI.Repository;

/// <summary>
/// The coupon repository interface.
/// </summary>
public interface ICouponRepository
{
    /// <summary>
    /// The get coupon.
    /// </summary>
    /// <param name="couponName">
    /// The coupon name.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{CouponDto}"/>
    /// </returns>
    Task<CouponDto> GetCoupon(string couponName);
}