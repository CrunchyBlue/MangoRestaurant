using Mango.Services.ProductAPI.Models.Dtos;

namespace Mango.Services.ProductAPI.Repository;

/// <summary>
/// The product repository interface.
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// The get products.
    /// </summary>
    /// <returns>
    /// The <see cref="T:Task{IEnumerable{ProductDto}}"/>
    /// </returns>
    public Task<IEnumerable<ProductDto>> GetProducts();
    
    /// <summary>
    /// The get product by id.
    /// </summary>
    /// <param name="productId">
    /// The product id.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{ProductDto}"/>
    /// </returns>
    public Task<ProductDto> GetProductById(int productId);

    /// <summary>
    /// The create update product.
    /// </summary>
    /// <param name="productDto">
    /// The product dto.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{ProductDto}"/>
    /// </returns>
    public Task<ProductDto> CreateUpdateProduct(ProductDto productDto);

    /// <summary>
    /// The delete product.
    /// </summary>
    /// <param name="productId">
    /// The product id.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{bool}"/>
    /// </returns>
    public Task<bool> DeleteProduct(int productId);
}