using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models;

/// <summary>
/// The product dto.
/// </summary>
public class ProductDto
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductDto"/> class.
    /// </summary>
    public ProductDto()
    {
        Count = 1;
    }
    
    /// <summary>
    /// Gets or sets the product id.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the price.
    /// </summary>
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
    
    /// <summary>
    /// Gets or sets the count.
    /// </summary>
    [Range(1,100)]
    public int Count { get; set; }
}