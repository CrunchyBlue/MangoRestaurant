using Mango.Web.Constants;
using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers;

/// <summary>
/// The product controller.
/// </summary>
[Authorize(Roles = "Admin")]
public class ProductController : Controller
{
    /// <summary>
    /// The product service.
    /// </summary>
    private readonly IProductService _productService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductController"/> class.
    /// </summary>
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// The product index.
    /// </summary>
    /// <returns>
    /// The <see cref="T:Task{IActionResult}"/>
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> ProductIndex()
    {
        List<ProductDto> products = new();
        var accessToken = await HttpContext.GetTokenAsync(SD.AccessToken);
        var response = await _productService.GetAllProductsAsync<ResponseDto>(accessToken);

        if (response is {IsSuccess: true})
        {
            products = JsonConvert.DeserializeObject<List<ProductDto>>(
                Convert.ToString(response.Result) ?? string.Empty);
        }

        return View(products);
    }

    public async Task<IActionResult> CreateProduct()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProduct(ProductDto product)
    {
        if (ModelState.IsValid)
        {
            var accessToken = await HttpContext.GetTokenAsync(SD.AccessToken);
            var response = await _productService.CreateProductAsync<ResponseDto>(product, accessToken);

            if (response is {IsSuccess: true})
            {
                return RedirectToAction(nameof(ProductIndex));
            }
        }

        return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> EditProduct(int productId)
    {
        var accessToken = await HttpContext.GetTokenAsync(SD.AccessToken);
        var response = await _productService.GetProductByIdAsync<ResponseDto>(productId, accessToken);

        if (response is {IsSuccess: true})
        {
            var product = JsonConvert.DeserializeObject<ProductDto>(
                Convert.ToString(response.Result) ?? string.Empty);
            return View(product);
        }

        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProduct(ProductDto product)
    {
        if (ModelState.IsValid)
        {
            var accessToken = await HttpContext.GetTokenAsync(SD.AccessToken);
            var response = await _productService.UpdateProductAsync<ResponseDto>(product, accessToken);

            if (response is {IsSuccess: true})
            {
                return RedirectToAction(nameof(ProductIndex));
            }
        }

        return View(product);
    }
   
    [HttpGet]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        var accessToken = await HttpContext.GetTokenAsync(SD.AccessToken);
        var response = await _productService.GetProductByIdAsync<ResponseDto>(productId, accessToken);

        if (response is {IsSuccess: true})
        {
            var product = JsonConvert.DeserializeObject<ProductDto>(
                Convert.ToString(response.Result) ?? string.Empty);
            return View(product);
        }

        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProduct(ProductDto product)
    {
        if (ModelState.IsValid)
        {
            var accessToken = await HttpContext.GetTokenAsync(SD.AccessToken);
            var response = await _productService.DeleteProductAsync<ResponseDto>(product.ProductId, accessToken);

            if (response is {IsSuccess: true})
            {
                return RedirectToAction(nameof(ProductIndex));
            }
        }

        return View(product);
    }
}