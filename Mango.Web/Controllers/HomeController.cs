using System.Diagnostics;
using Mango.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace Mango.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;
    private readonly ICartService _cartService;

    public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
    {
        _logger = logger;
        _productService = productService;
        _cartService = cartService;
    }

    public async Task<IActionResult> Index()
    {
        var products = new List<ProductDto>();
        var response = await _productService.GetAllProductsAsync<ResponseDto>(string.Empty);

        if (response is {IsSuccess: true})
        {
            products = JsonConvert.DeserializeObject<List<ProductDto>>(
                Convert.ToString(response.Result) ?? string.Empty);
        }

        return View(products);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Details(int productId)
    {
        var product = new ProductDto();
        var response = await _productService.GetProductByIdAsync<ResponseDto>(productId, string.Empty);

        if (response is {IsSuccess: true})
        {
            product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result) ?? string.Empty);
        }

        return View(product);
    }

    [Authorize]
    [HttpPost]
    [ActionName("Details")]
    public async Task<IActionResult> DetailsPost(ProductDto productDto)
    {
        var cartDto = new CartDto()
        {
            CartHeader = new CartHeaderDto()
            {
                UserId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value
            }
        };

        var cartDetails = new CartDetailDto()
        {
            Count = productDto.Count,
            ProductId = productDto.ProductId
        };

        var response = await _productService.GetProductByIdAsync<ResponseDto>(productDto.ProductId, "");

        if (response is {IsSuccess: true})
        {
            cartDetails.Product =
                JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result) ?? string.Empty);
        }

        var cartDetailDtos = new List<CartDetailDto> {cartDetails};

        cartDto.CartDetails = cartDetailDtos;
        
        var accessToken = await HttpContext.GetTokenAsync(SD.AccessToken);
        var addToCartResponse = await _cartService.AddToCartAsync<ResponseDto>(cartDto, accessToken);

        if (addToCartResponse is {IsSuccess: true})
        {
            return RedirectToAction(nameof(Index));
        }

        return View(productDto);
    }

    [Authorize]
    public IActionResult Login()
    {
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Logout()
    {
        return SignOut("Cookies", "oidc");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}