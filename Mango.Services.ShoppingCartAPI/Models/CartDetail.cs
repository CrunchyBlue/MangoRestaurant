using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.ShoppingCartAPI.Models;

/// <summary>
/// The cart detail model.
/// </summary>
public class CartDetail
{
    /// <summary>
    /// Gets or sets the cart details id.
    /// </summary>
    [Key]
    public int CartDetailId { get; set; }

    /// <summary>
    /// Gets or sets the cart header id.
    /// </summary>
    public int CartHeaderId { get; set; }
    
    /// <summary>
    /// Gets or sets the cart header.
    /// </summary>
    [ForeignKey("CartHeaderId")]
    public virtual CartHeader CartHeader { get; set; }
    
    /// <summary>
    /// Gets or sets the product id.
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Gets or sets the product.
    /// </summary>
    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }

    /// <summary>
    /// Gets or sets the count.
    /// </summary>
    public int Count { get; set; }
}