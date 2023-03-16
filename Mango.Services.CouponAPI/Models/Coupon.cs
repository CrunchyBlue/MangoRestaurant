using System.ComponentModel.DataAnnotations;

namespace Mango.Services.CouponAPI.Models;

/// <summary>
/// The coupon model.
/// </summary>
public class Coupon
{
    /// <summary>
    /// Gets or sets the coupon id.
    /// </summary>
    [Key] 
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