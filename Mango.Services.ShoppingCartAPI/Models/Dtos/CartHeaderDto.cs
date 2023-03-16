namespace Mango.Services.ShoppingCartAPI.Models.Dtos;

/// <summary>
/// The cart header dto model.
/// </summary>
public class CartHeaderDto
{
    /// <summary>
    /// Gets or sets the cart header id.
    /// </summary>
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