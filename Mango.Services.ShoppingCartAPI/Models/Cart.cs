namespace Mango.Services.ShoppingCartAPI.Models;

/// <summary>
/// The cart model.
/// </summary>
public class Cart
{
    /// <summary>
    /// Gets or sets the cart header.
    /// </summary>
    public CartHeader CartHeader { get; set; }
    
    /// <summary>
    /// Gets or sets the cart details.
    /// </summary>
    public IEnumerable<CartDetail> CartDetails { get; set; }
}