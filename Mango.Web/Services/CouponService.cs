using Mango.Web.Constants;
using Mango.Web.Models;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services;

/// <summary>
/// The coupon service.
/// </summary>
public class CouponService : BaseService, ICouponService
{
    /// <summary>
    /// The http client factory.
    /// </summary>
    private readonly IHttpClientFactory _httpClientFactory;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="CouponService"/> class.
    /// </summary>
    /// <param name="httpClientFactory"></param>
    public CouponService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    /// <inheritdoc cref="ICouponService.GetCoupon"/>
    public async Task<T> GetCoupon<T>(string couponCode, string token)
    {
        return await SendAsync<T>(new Request
        {
            ApiType = SD.ApiType.GET,
            Url = $"{SD.CouponAPIBase}/api/coupon/{couponCode}",
            AccessToken = token
        });
    }
}