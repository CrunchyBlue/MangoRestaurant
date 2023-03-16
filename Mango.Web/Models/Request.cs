using static Mango.Web.Constants.SD;

namespace Mango.Web.Models;

/// <summary>
/// The request.
/// </summary>
public class Request
{
    /// <summary>
    /// Gets or sets the api type.
    /// </summary>
    public ApiType ApiType { get; set; } = ApiType.GET;
    
    /// <summary>
    /// Gets or sets the url.
    /// </summary>
    public string Url { get; set; }
    
    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    public object Data { get; set; }
    
    /// <summary>
    /// Gets or sets the access token.
    /// </summary>
    public string AccessToken { get; set; }
}