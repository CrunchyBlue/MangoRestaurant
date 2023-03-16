using Mango.Services.ShoppingCartAPI.Models.Dtos;
using Newtonsoft.Json;

namespace Mango.Services.ShoppingCartAPI.Repository;

/// <summary>
/// The coupon repository.
/// </summary>
public class CouponRepository : ICouponRepository
{
    /// <summary>
    /// The http client.
    /// </summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="CouponRepository"/> class.
    /// </summary>
    /// <param name="httpClient">
    /// The http client.
    /// </param>
    public CouponRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    /// <summary>
    /// The get coupon.
    /// </summary>
    /// <param name="couponCode">
    /// The coupon code.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{CouponDto}"/>
    /// </returns>
    public async Task<CouponDto> GetCoupon(string couponCode)
    {
        var httpResponse = await _httpClient.GetAsync($"/api/coupon/{couponCode}");
        var content = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<ResponseDto>(content);

        if (response is {IsSuccess: true})
        {
            return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result) ?? string.Empty);
        }

        return new CouponDto();
    }
}