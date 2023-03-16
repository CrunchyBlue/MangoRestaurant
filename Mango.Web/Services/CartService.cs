using Mango.Web.Constants;
using Mango.Web.Models;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services;

/// <summary>
/// The cart service.
/// </summary>
public class CartService : BaseService, ICartService
{
    /// <summary>
    /// The http client factory.
    /// </summary>
    private readonly IHttpClientFactory _httpClientFactory;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="CartService"/> class.
    /// </summary>
    /// <param name="httpClientFactory"></param>
    public CartService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    /// <inheritdoc cref="ICartService.GetCartByUserId"/>
    public async Task<T> GetCartByUserIdAsync<T>(string userId, string token)
    {
        return await SendAsync<T>(new Request
        {
            ApiType = SD.ApiType.GET,
            Url = $"{SD.ShoppingCartAPIBase}/api/cart/GetCart/{userId}",
            AccessToken = token
        });
    }

    /// <inheritdoc cref="ICartService.AddToCartAsync"/>
    public async Task<T> AddToCartAsync<T>(CartDto cartDto, string token)
    {
        return await SendAsync<T>(new Request
        {
            ApiType = SD.ApiType.POST,
            Data = cartDto,
            Url = SD.ShoppingCartAPIBase + "/api/cart/AddCart",
            AccessToken = token
        });
    }
    
    /// <inheritdoc cref="ICartService.UpdateCartAsync"/>
    public async Task<T> UpdateCartAsync<T>(CartDto cartDto, string token)
    {
        return await SendAsync<T>(new Request
        {
            ApiType = SD.ApiType.POST,
            Data = cartDto,
            Url = SD.ShoppingCartAPIBase + "/api/cart/UpdateCart",
            AccessToken = token
        });
    }
    
    /// <inheritdoc cref="ICartService.RemoveFromCartAsync"/>
    public async Task<T> RemoveFromCartAsync<T>(int cartDetailId, string token)
    {
        return await SendAsync<T>(new Request
        {
            ApiType = SD.ApiType.POST,
            Data = cartDetailId,
            Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCart",
            AccessToken = token
        });
    }
    
    /// <inheritdoc cref="ICartService.ClearCartAsync"/>
    public async Task<T> ClearCartAsync<T>(string userId, string token)
    {
        return await SendAsync<T>(new Request
        {
            ApiType = SD.ApiType.POST,
            Data = userId,
            Url = SD.ShoppingCartAPIBase + "/api/cart/ClearCart",
            AccessToken = token
        });
    }

    /// <inheritdoc cref="ICartService.ApplyCouponAsync"/>
    public async Task<T> ApplyCouponAsync<T>(CartDto cartDto, string token)
    {
        return await SendAsync<T>(new Request
        {
            ApiType = SD.ApiType.POST,
            Data = cartDto,
            Url = SD.ShoppingCartAPIBase + "/api/cart/ApplyCoupon",
            AccessToken = token
        });
    }

    /// <inheritdoc cref="ICartService.RemoveCouponAsync"/>
    public async Task<T> RemoveCouponAsync<T>(string userId, string token)
    {
        return await SendAsync<T>(new Request
        {
            ApiType = SD.ApiType.POST,
            Data = userId,
            Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCoupon",
            AccessToken = token
        });
    }

    /// <inheritdoc cref="ICartService.CheckoutAsync"/>
    public async Task<T> CheckoutAsync<T>(CartHeaderDto cartHeader, string token)
    {
        return await SendAsync<T>(new Request
        {
            ApiType = SD.ApiType.POST,
            Data = cartHeader,
            Url = SD.ShoppingCartAPIBase + "/api/cart/Checkout",
            AccessToken = token
        });
    }
}