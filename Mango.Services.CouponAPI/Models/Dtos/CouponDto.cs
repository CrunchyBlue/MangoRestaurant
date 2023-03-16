namespace Mango.Services.CouponAPI.Models.Dtos;

/// <summary>
/// The coupon dto model.
/// </summary>
public class CouponDto
{
    /// <summary>
    /// Gets or sets the coupon id.
    /// </summary>
    public int CouponId { get; set; }

    /// <summary>
    /// Gets or sets the coupon code.
    /// </summary>
    public string CouponCode { get; set; }
    
    /// <summary>
    /// Gets or sets the discount amount.
    /// </summary>
    public double DiscountAmount { get; set; }
}