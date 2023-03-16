namespace Mango.Web.Services.IServices;

/// <summary>
/// The cart service interface.
/// </summary>
public interface ICouponService : IBaseService
{
    /// <summary>
    /// The get coupon async.
    /// </summary>
    /// <typeparam name="T">
    /// The generic type.
    /// </typeparam>
    /// <param name="couponCode">
    /// The coupon code.
    /// </param>
    /// <param name="token">
    /// The token.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{T}"/>
    /// </returns>
    public Task<T> GetCoupon<T>(string couponCode, string token = null);
}