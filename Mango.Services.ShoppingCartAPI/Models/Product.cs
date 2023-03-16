using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.ShoppingCartAPI.Models;

/// <summary>
/// The product model.
/// </summary>
public class Product
{
    /// <summary>
    /// Gets or sets the product id.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the price.
    /// </summary>
    [Range(1, 1000)]
    public double Price { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the category name.
    /// </summary>
    public string CategoryName { get; set; }

    /// <summary>
    /// Gets or sets the image url.
    /// </summary>
    public string ImageUrl { get; set; }
}