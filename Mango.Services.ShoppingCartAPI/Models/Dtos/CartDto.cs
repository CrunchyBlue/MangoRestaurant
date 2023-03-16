namespace Mango.Services.ShoppingCartAPI.Models.Dtos;

/// <summary>
/// The cart dto model.
/// </summary>
public class CartDto
{
    /// <summary>
    /// Gets or sets the cart header.
    /// </summary>
    public CartHeaderDto CartHeader { get; set; }
    
    /// <summary>
    /// Gets or sets the cart details.
    /// </summary>
    public IEnumerable<CartDetailDto> CartDetails { get; set; }
}