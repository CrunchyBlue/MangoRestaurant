using Mango.Services.CouponAPI.Models.Dtos;
using Mango.Services.CouponAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers;

/// <summary>
/// The coupon api controller.
/// </summary>
[ApiController]
[Route("api/coupon")]
public class CouponApiController : ControllerBase
{
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<CouponApiController> _logger;

    /// <summary>
    /// The product repository.
    /// </summary>
    private readonly ICouponRepository _couponRepository;

    /// <summary>
    /// The response.
    /// </summary>
    private readonly ResponseDto _response;

    /// <summary>
    /// Initializes a new instance of the <see cref="CouponApiController"/> class.
    /// </summary>
    /// <param name="logger">
    /// The logger.
    /// </param>
    /// <param name="couponRepository">
    /// The coupon repository.
    /// </param>
    public CouponApiController(ILogger<CouponApiController> logger, ICouponRepository couponRepository)
    {
        _logger = logger;
        _couponRepository = couponRepository;
        _response = new ResponseDto();
    }

    /// <summary>
    /// The get coupon by code.
    /// </summary>
    /// <param name="couponCode">
    /// The coupon code.
    /// </param>
    /// <returns>
    /// The <see cref="T:Task{object}"/>
    /// </returns>
    [HttpGet("{couponCode}")]
    public async Task<object> GetCouponByCode(string couponCode)
    {
        try
        {
            var coupon = await _couponRepository.GetCouponByCode(couponCode);
            _response.Result = coupon;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() {e.ToString()};
        }

        return _response;
    }
}