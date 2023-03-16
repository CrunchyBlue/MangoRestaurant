namespace Mango.Web.Models;

/// <summary>
/// The response dto.
/// </summary>
public class ResponseDto
{
    /// <summary>
    /// Gets or sets a value indicating whether response is successful.
    /// </summary>
    public bool IsSuccess { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the result.
    /// </summary>
    public object Result { get; set; }
    
    /// <summary>
    /// Gets or sets the display message.
    /// </summary>
    public string DisplayMessage { get; set; } = "";
    
    /// <summary>
    /// Gets or sets the error messages.
    /// </summary>
    public List<string> ErrorMessages { get; set; }
}