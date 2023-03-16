namespace Mango.Services.OrderProcessor.Messages;

/// <summary>
/// The product dto model.
/// </summary>
public class ProductDto
{
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
}