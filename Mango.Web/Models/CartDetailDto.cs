namespace Mango.Web.Models;

/// <summary>
/// The cart detail dto model.
/// </summary>
public class CartDetailDto
{
    /// <summary>
    /// Gets or sets the cart details id.
    /// </summary>
    public int CartDetailId { get; set; }

    /// <summary>
    /// Gets or sets the cart header id.
    /// </summary>
    public int CartHeaderId { get; set; }
    
    /// <summary>
    /// Gets or sets the cart header.
    /// </summary>
    public virtual CartHeaderDto CartHeader { get; set; }
    
    /// <summary>
    /// Gets or sets the product id.
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Gets or sets the product.
    /// </summary>
    public virtual ProductDto Product { get; set; }

    /// <summary>
    /// Gets or sets the count.
    /// </summary>
    public int Count { get; set; }
}