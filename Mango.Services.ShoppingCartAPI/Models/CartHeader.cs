using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ShoppingCartAPI.Models;

/// <summary>
/// The cart header model.
/// </summary>
public class CartHeader
{
    /// <summary>
    /// Gets or sets the cart header id.
    /// </summary>
    [Key]
    public int CartHeaderId { get; set; }

    /// <summary>
    /// Gets or sets the user id.
    /// </summary>
    public string UserId { get; set; }
    
    /// <summary>
    /// Gets or sets the coupon code.
    /// </summary>
    public string CouponCode { get; set; }
}