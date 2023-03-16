using Mango.Services.ProductAPI.Constants;
using Mango.Services.ProductAPI.Models.Dtos;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers;

/// <summary>
/// The product controller.
/// </summary>
[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<ProductController> _logger;

    /// <summary>
    /// The product repository.
    /// </summary>
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// The response.
    /// </summary>
    private readonly ResponseDto _response;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductController"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    /// <param name="productRepository">
    /// The product repository.
    /// </param>
    public ProductController(ILogger<ProductController> logger, IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
        _response = new ResponseDto();
    }

    /// <summary>
    /// The get.
    /// </summary>
    /// <returns>
    /// The <see cref="T:Task{object}"/>
    /// </returns>
    [HttpGet]
    public async Task<object> Get()
    {
        try
        {
            var products = await _productRepository.GetProducts();
            _response.Result = products;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string> {e.ToString()};
        }

        return _response;
    }
    
    /// <summary>
    /// The get.
    /// </summary>
    /// <param name="productId">
    /// The product id.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{object}"/>
    /// </returns>
    [HttpGet("{productId:int}")]
    public async Task<object> Get(int productId)
    {
        try
        {
            var product = await _productRepository.GetProductById(productId);
            _response.Result = product;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string> {e.ToString()};
        }

        return _response;
    }
    
    /// <summary>
    /// The post.
    /// </summary>
    /// <param name="productForCreation">
    /// The product for creation.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{object}"/>
    /// </returns>
    [Authorize]
    [HttpPost]
    public async Task<object> Post([FromBody] ProductDto productForCreation)
    {
        try
        {
            var product = await _productRepository.CreateUpdateProduct(productForCreation);
            _response.Result = product;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string> {e.ToString()};
        }

        return _response;
    }
    
    /// <summary>
    /// The put.
    /// </summary>
    /// <param name="productForUpdate">
    /// The product for update.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{object}"/>
    /// </returns>
    [Authorize]
    [HttpPut]
    public async Task<object> Put([FromBody] ProductDto productForUpdate)
    {
        try
        {
            var product = await _productRepository.CreateUpdateProduct(productForUpdate);
            _response.Result = product;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string> {e.ToString()};
        }

        return _response;
    }
    
    /// <summary>
    /// The delete.
    /// </summary>
    /// <param name="productId">
    /// The product id.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{object}"/>
    /// </returns>
    [Authorize(Roles = SD.Admin)]
    [HttpDelete("{productId:int}")]
    public async Task<object> Delete(int productId)
    {
        try
        {
            var isSuccess = await _productRepository.DeleteProduct(productId);
            _response.IsSuccess = isSuccess;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string> {e.ToString()};
        }

        return _response;
    }
}