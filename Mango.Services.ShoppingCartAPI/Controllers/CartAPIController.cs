using Mango.MessageBus;
using Mango.Services.ShoppingCartAPI.Constants;
using Mango.Services.ShoppingCartAPI.Messages;
using Mango.Services.ShoppingCartAPI.Models.Dtos;
using Mango.Services.ShoppingCartAPI.Repository;
using Mango.Services.ShoppingCartAPI.Senders;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ShoppingCartAPI.Controllers;

/// <summary>
/// The cart api controller.
/// </summary>
[ApiController]
[Route("api/cart")]
public class CartAPIController : ControllerBase
{
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<CartAPIController> _logger;

    /// <summary>
    /// The cart repository.
    /// </summary>
    private readonly ICartRepository _cartRepository;

    /// <summary>
    /// The coupon repository.
    /// </summary>
    private readonly ICouponRepository _couponRepository;

    /// <summary>
    /// Azure Service Bus
    /// The message bus.
    /// </summary>
    private readonly IMessageBus _messageBus;

    /// <summary>
    /// RabbitMQ
    /// The rabbitmq sender.
    /// </summary>
    // private readonly IRabbitMQSender _rabbitMQSender;

    /// <summary>
    /// The response.
    /// </summary>
    private readonly ResponseDto _response;

    /// <summary>
    /// Initializes a new instance of the <see cref="CartAPIController"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    /// <param name="cartRepository">
    /// The cart repository.
    /// </param>
    /// <param name="couponRepository">
    /// The coupon repository.
    /// </param>
    /// <param name="rabbitMQSender">
    /// The rabbitmq sender.
    /// </param>
    public CartAPIController(ILogger<CartAPIController> logger, ICartRepository cartRepository,
        ICouponRepository couponRepository,
        // Azure Service Bus
        IMessageBus messageBus
        
        // RabbitMQ
        // IRabbitMQSender rabbitMQSender
        )
    {
        _logger = logger;
        _cartRepository = cartRepository;
        _couponRepository = couponRepository;

        // Azure Service Bus
        _messageBus = messageBus;

        // RabbitMQ
        // _rabbitMQSender = rabbitMQSender;

        _response = new ResponseDto();
    }

    /// <summary>
    /// The get cart.
    /// </summary>
    /// <param name="userId">
    /// The user id.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{object}"/>
    /// </returns>
    [HttpGet("GetCart/{userId}")]
    public async Task<object> GetCart(string userId)
    {
        try
        {
            var cartDto = await _cartRepository.GetCartByUserId(userId);
            _response.Result = cartDto;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() {e.ToString()};
        }

        return _response;
    }

    /// <summary>
    /// The add cart.
    /// </summary>
    /// <param name="cart">
    /// The cart.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{object}"/>
    /// </returns>
    [HttpPost("AddCart")]
    public async Task<object> AddCart([FromBody] CartDto cart)
    {
        try
        {
            var cartDto = await _cartRepository.CreateUpdateCart(cart);
            _response.Result = cartDto;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() {e.ToString()};
        }

        return _response;
    }

    /// <summary>
    /// The update cart.
    /// </summary>
    /// <param name="cart">
    /// The cart.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{object}"/>
    /// </returns>
    [HttpPost("UpdateCart")]
    public async Task<object> UpdateCart([FromBody] CartDto cart)
    {
        try
        {
            var cartDto = await _cartRepository.CreateUpdateCart(cart);
            _response.Result = cartDto;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() {e.ToString()};
        }

        return _response;
    }

    /// <summary>
    /// The remove cart.
    /// </summary>
    /// <param name="cartDetailId">
    /// The cart detail id.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{object}"/>
    /// </returns>
    [HttpPost("RemoveCart")]
    public async Task<object> RemoveCart([FromBody] int cartDetailId)
    {
        try
        {
            var isSuccess = await _cartRepository.RemoveFromCart(cartDetailId);
            _response.Result = isSuccess;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() {e.ToString()};
        }

        return _response;
    }

    /// <summary>
    /// The clear cart.
    /// </summary>
    /// <param name="userId">
    /// The user id.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{object}"/>
    /// </returns>
    [HttpPost("ClearCart")]
    public async Task<object> ClearCart([FromBody] string userId)
    {
        try
        {
            var isSuccess = await _cartRepository.ClearCart(userId);
            _response.Result = isSuccess;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() {e.ToString()};
        }

        return _response;
    }

    /// <summary>
    /// The apply coupon.
    /// </summary>
    /// <param name="cart">
    /// The cart.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{object}"/>
    /// </returns>
    [HttpPost("ApplyCoupon")]
    public async Task<object> ApplyCoupon([FromBody] CartDto cart)
    {
        try
        {
            var isSuccess = await _cartRepository.ApplyCoupon(cart.CartHeader.UserId, cart.CartHeader.CouponCode);
            _response.Result = isSuccess;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() {e.ToString()};
        }

        return _response;
    }

    /// <summary>
    /// The remove coupon.
    /// </summary>
    /// <param name="userId">
    /// The user id.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{object}"/>
    /// </returns>
    [HttpPost("RemoveCoupon")]
    public async Task<object> RemoveCoupon([FromBody] string userId)
    {
        try
        {
            var isSuccess = await _cartRepository.RemoveCoupon(userId);
            _response.Result = isSuccess;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() {e.ToString()};
        }

        return _response;
    }

    /// <summary>
    /// The checkout.
    /// </summary>
    /// <param name="userId">
    /// The user id.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{object}"/>
    /// </returns>
    [HttpPost("Checkout")]
    public async Task<object> Checkout([FromBody] CheckoutHeaderDto checkoutHeader)
    {
        try
        {
            var cart = await _cartRepository.GetCartByUserId(checkoutHeader.UserId);

            if (cart == null)
            {
                return BadRequest();
            }

            if (!string.IsNullOrWhiteSpace(checkoutHeader.CouponCode))
            {
                var coupon = await _couponRepository.GetCoupon(checkoutHeader.CouponCode);

                if (Math.Abs(checkoutHeader.DiscountTotal - coupon.DiscountAmount) > 0)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>()
                    {
                        "Coupon price has changed, please confirm your order."
                    };
                    _response.DisplayMessage = "Coupon price has changed, please confirm your order.";
                    return _response;
                }
            }

            checkoutHeader.CartDetails = cart.CartDetails;

            // Azure Service Bus
            await _messageBus.PublishMessage(checkoutHeader, SD.CheckoutTopic, SD.ServiceBusConnectionString);

            // RabbitMQ
            // _rabbitMQSender.SendMessage(checkoutHeader, SD.CheckoutTopic);

            await _cartRepository.ClearCart(checkoutHeader.UserId);
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() {e.ToString()};
        }

        return _response;
    }
}