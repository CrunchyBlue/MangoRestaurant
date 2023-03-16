using Mango.Web.Models;

namespace Mango.Web.Services.IServices;

/// <summary>
/// The product service interface.
/// </summary>
public interface IProductService : IBaseService
{
    /// <summary>
    /// The get all products async.
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
    public Task<T> GetAllProductsAsync<T>(string token);
    
    /// <summary>
    /// The get product by id async.
    /// </summary>
    /// <typeparam name="T">
    /// The generic type.
    /// </typeparam>
    /// <param name="productId">
    /// The product id.
    /// </param>
    /// <param name="token">
    /// The token.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{T}"/>
    /// </returns>
    public Task<T> GetProductByIdAsync<T>(int productId, string token);
    
    /// <summary>
    /// The create product async.
    /// </summary>
    /// <typeparam name="T">
    /// The generic type.
    /// </typeparam>
    /// <param name="productDto">
    /// The product dto.
    /// </param>
    /// <param name="token">
    /// The token.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{T}"/>
    /// </returns>
    public Task<T> CreateProductAsync<T>(ProductDto productDto, string token);
    
    /// <summary>
    /// The update product async.
    /// </summary>
    /// <typeparam name="T">
    /// The generic type.
    /// </typeparam>
    /// <param name="productDto">
    /// The product dto.
    /// </param>
    /// <param name="token">
    /// The token.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{T}"/>
    /// </returns>
    public Task<T> UpdateProductAsync<T>(ProductDto productDto, string token);

    /// <summary>
    /// The delete product async.
    /// </summary>
    /// <typeparam name="T">
    /// The generic type.
    /// </typeparam>
    /// <param name="productId">
    /// The product id.
    /// </param>
    /// <param name="token">
    /// The token.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{T}"/>
    /// </returns>
    public Task<T> DeleteProductAsync<T>(int productId, string token);
}