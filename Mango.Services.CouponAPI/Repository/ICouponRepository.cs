using Mango.Services.CouponAPI.Models.Dtos;

namespace Mango.Services.CouponAPI.Repository;

/// <summary>
/// The coupon repository interface.
/// </summary>
public interface ICouponRepository
{
    /// <summary>
    /// The get coupon by code.
    /// </summary>
    /// <param name="couponCode">
    /// The coupon code.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{CouponDto}"/>
    /// </returns>
    Task<CouponDto> GetCouponByCode(string couponCode);
}