using Mango.Web.Constants;
using Mango.Web.Models;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services;

/// <summary>
/// The product service.
/// </summary>
public class ProductService : BaseService, IProductService
{
    /// <summary>
    /// The http client factory.
    /// </summary>
    private readonly IHttpClientFactory _httpClientFactory;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductService"/> class.
    /// </summary>
    /// <param name="httpClientFactory"></param>
    public ProductService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    /// <inheritdoc cref="IProductService.GetAllProductsAsync"/>
    public async Task<T> GetAllProductsAsync<T>(string token)
    {
        return await SendAsync<T>(new Request
        {
            ApiType = SD.ApiType.GET,
            Url = $"{SD.ProductAPIBase}/api/products",
            AccessToken = token
        });
    }

    /// <inheritdoc cref="IProductService.GetProductByIdAsync"/>
    public async Task<T> GetProductByIdAsync<T>(int productId, string token)
    {
        return await SendAsync<T>(new Request
        {
            ApiType = SD.ApiType.GET,
            Url = $"{SD.ProductAPIBase}/api/products/{productId}",
            AccessToken = token
        });
    }

    /// <inheritdoc cref="IProductService.CreateProductAsync"/>
    public async Task<T> CreateProductAsync<T>(ProductDto productDto, string token)
    {
        return await SendAsync<T>(new Request
        {
            ApiType = SD.ApiType.POST,
            Data = productDto,
            Url = SD.ProductAPIBase + "/api/products",
            AccessToken = token
        });
    }

    /// <inheritdoc cref="IProductService.UpdateProductAsync"/>
    public async Task<T> UpdateProductAsync<T>(ProductDto productDto, string token)
    {
        return await SendAsync<T>(new Request
        {
            ApiType = SD.ApiType.PUT,
            Data = productDto,
            Url = $"{SD.ProductAPIBase}/api/products",
            AccessToken = token
        });
    }

    /// <inheritdoc cref="IProductService.DeleteProductAsync"/>
    public async Task<T> DeleteProductAsync<T>(int productId, string token)
    {
        return await SendAsync<T>(new Request
        {
            ApiType = SD.ApiType.DELETE,
            Url = $"{SD.ProductAPIBase}/api/products/{productId}",
            AccessToken = token
        });
    }
}