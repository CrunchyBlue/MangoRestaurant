using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.OrderProcessor.Models;

public class OrderDetail
{
    /// <summary>
    /// Gets or sets the order details id.
    /// </summary>
    [Key]
    public int OrderDetailId { get; set; }

    /// <summary>
    /// Gets or sets the order header id.
    /// </summary>
    public int OrderHeaderId { get; set; }
    
    /// <summary>
    /// Gets or sets the order header.
    /// </summary>
    [ForeignKey("OrderHeaderId")]
    public virtual OrderHeader OrderHeader { get; set; }
    
    /// <summary>
    /// Gets or sets the product id.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    public string ProductName { get; set; }

    /// <summary>
    /// Gets or sets the price.
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Gets or sets the count.
    /// </summary>
    public int Count { get; set; }
}