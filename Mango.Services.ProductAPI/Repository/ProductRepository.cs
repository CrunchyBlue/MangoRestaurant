using AutoMapper;
using Mango.Services.ProductAPI.DbContexts;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Repository;

/// <summary>
/// The product repository.
/// </summary>
public class ProductRepository : IProductRepository
{
    /// <summary>
    /// The db context.
    /// </summary>
    private readonly ApplicationDbContext _dbContext;

    /// <summary>
    /// The mapper.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductRepository"/> class.
    /// </summary>
    /// <param name="dbContext">
    /// The db context.
    /// </param>
    /// <param name="mapper">
    /// The mapper.
    /// </param>
    public ProductRepository(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    /// <inheritdoc cref="IProductRepository.GetProducts"/>
    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        var products = await _dbContext.Products.ToListAsync();

        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    /// <inheritdoc cref="IProductRepository.GetProductById"/>
    public async Task<ProductDto> GetProductById(int productId)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync((p) => p.ProductId == productId);
        return _mapper.Map<ProductDto>(product);
    }

    /// <inheritdoc cref="IProductRepository.CreateUpdateProduct"/>
    public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);

        if (product.ProductId > 0)
        {
            _dbContext.Products.Update(product);
        }
        else
        {
            _dbContext.Products.Add(product);
        }

        await _dbContext.SaveChangesAsync();
        return _mapper.Map<ProductDto>(product);
    }

    /// <inheritdoc cref="IProductRepository.DeleteProduct"/>
    public async Task<bool> DeleteProduct(int productId)
    {
        try
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product == null)
            {
                return false;
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}