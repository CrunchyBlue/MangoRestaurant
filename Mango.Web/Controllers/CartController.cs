using Mango.Web.Constants;
using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers;

/// <summary>
/// The cart controller.
/// </summary>
public class CartController : Controller
{
    /// <summary>
    /// The cart service.
    /// </summary>
    private readonly ICartService _cartService;

    /// <summary>
    /// The product service.
    /// </summary>
    private readonly IProductService _productService;

    /// <summary>
    /// The coupon service.
    /// </summary>
    private readonly ICouponService _couponService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CartController"/> class.
    /// </summary>
    public CartController(ICartService cartService, IProductService productService, ICouponService couponService)
    {
        _cartService = cartService;
        _productService = productService;
        _couponService = couponService;
    }

    /// <summary>
    /// The cart index.
    /// </summary>
    /// <returns>
    /// The <see cref="IActionResult"/>
    /// </returns>
    public async Task<IActionResult> CartIndex()
    {
        return View(await LoadCartDtoBasedOnLoggedInUser());
    }

    /// <summary>
    /// The load cart dto based on logged in user.
    /// </summary>
    /// <returns>
    /// The <see cref="T:Task{CartDto}"/>
    /// </returns>
    private async Task<CartDto> LoadCartDtoBasedOnLoggedInUser()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        var accessToken = await HttpContext.GetTokenAsync(SD.AccessToken);
        var response = await _cartService.GetCartByUserIdAsync<ResponseDto>(userId, accessToken);

        var cartDto = new CartDto();
        if (response is {IsSuccess: true})
        {
            cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result) ?? string.Empty);
        }

        if (cartDto is {CartHeader: { }} and {CartDetails: { }})
        {
            if (!string.IsNullOrWhiteSpace(cartDto.CartHeader.CouponCode))
            {
                var couponResponse = await _couponService.GetCoupon<ResponseDto>(cartDto.CartHeader.CouponCode, accessToken);

                if (couponResponse is {IsSuccess: true})
                {
                    var coupon = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(couponResponse.Result) ?? string.Empty);
                    cartDto.CartHeader.DiscountTotal = coupon?.DiscountAmount ?? 0;
                }
            }

            foreach (var detail in cartDto.CartDetails)
            {
                cartDto.CartHeader.OrderTotal += (detail.Product.Price * detail.Count);
            }

            cartDto.CartHeader.OrderTotal -= cartDto.CartHeader.DiscountTotal;
            cartDto.CartHeader.OrderTotal = Math.Max(0, cartDto.CartHeader.OrderTotal);
        }

        return cartDto;
    }

    /// <summary>
    /// The remove.
    /// </summary>
    /// <param name="cartDetailId">
    /// The cart detail id.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{IActionResult}"/>
    /// </returns>
    public async Task<IActionResult> Remove(int cartDetailId)
    {
        var accessToken = await HttpContext.GetTokenAsync(SD.AccessToken);
        var response = await _cartService.RemoveFromCartAsync<ResponseDto>(cartDetailId, accessToken);

        if (response is not {IsSuccess: true})
        {
            foreach (var errorMessage in response.ErrorMessages)
            {
                Console.WriteLine(errorMessage);
            }
        }

        return RedirectToAction(nameof(CartIndex));
    }

    /// <summary>
    /// The apply coupon.
    /// </summary>
    /// <param name="cart">
    /// The cart.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{IActionResult}"/>
    /// </returns>
    [HttpPost]
    [ActionName("ApplyCoupon")]
    public async Task<IActionResult> ApplyCoupon(CartDto cart)
    {
        var accessToken = await HttpContext.GetTokenAsync(SD.AccessToken);
        var response = await _cartService.ApplyCouponAsync<ResponseDto>(cart, accessToken);

        if (response is not {IsSuccess: true})
        {
            foreach (var errorMessage in response.ErrorMessages)
            {
                Console.WriteLine(errorMessage);
            }
        }

        return RedirectToAction(nameof(CartIndex));
    }

    /// <summary>
    /// The remove coupon.
    /// </summary>
    /// <param name="cart">
    /// The cart.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{IActionResult}"/>
    /// </returns>
    [HttpPost]
    [ActionName("RemoveCoupon")]
    public async Task<IActionResult> RemoveCoupon(CartDto cart)
    {
        var accessToken = await HttpContext.GetTokenAsync(SD.AccessToken);
        var response = await _cartService.RemoveCouponAsync<ResponseDto>(cart.CartHeader.UserId, accessToken);

        if (response is not {IsSuccess: true})
        {
            foreach (var errorMessage in response.ErrorMessages)
            {
                Console.WriteLine(errorMessage);
            }
        }

        return RedirectToAction(nameof(CartIndex));
    }

    /// <summary>
    /// The checkout.
    /// </summary>
    /// <returns>
    /// The <see cref="T:Task{IActionResult}"/>
    /// </returns>
    public async Task<IActionResult> Checkout()
    {
        return View(await LoadCartDtoBasedOnLoggedInUser());
    }
    
    [HttpPost]
    public async Task<IActionResult> Checkout(CartDto cart)
    {
        try
        {
            var accessToken = await HttpContext.GetTokenAsync(SD.AccessToken);
            var response = await _cartService.CheckoutAsync<ResponseDto>(cart.CartHeader, accessToken);

            if (response is {IsSuccess: false})
            {
                TempData["Error"] = response.DisplayMessage;
                return View(await LoadCartDtoBasedOnLoggedInUser());
            }
            
            return RedirectToAction(nameof(Confirmation));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return View(cart);
        }
    }
    
    public async Task<IActionResult> Confirmation()
    {
        return View();
    }
}