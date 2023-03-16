namespace Mango.Web.Constants;

/// <summary>
/// The static details.
/// </summary>
public static class SD
{
    /// <summary>
    /// Gets or sets the product api base.
    /// </summary>
    public static string ProductAPIBase { get; set; }
    
    /// <summary>
    /// Gets or sets the shopping cart api base.
    /// </summary>
    public static string ShoppingCartAPIBase { get; set; }
    
    /// <summary>
    /// Gets or sets the coupon api base.
    /// </summary>
    public static string CouponAPIBase { get; set; }
    
    /// <summary>
    /// The client id constant
    /// </summary>
    public const string ClientId = "mango";
    
    /// <summary>
    /// The client secret constant
    /// </summary>
    public const string ClientSecret = "secret";
    
    /// <summary>
    /// The response type constant
    /// </summary>
    public const string ResponseType = "code";
    
    /// <summary>
    /// The name claim type constant
    /// </summary>
    public const string NameClaimType = "name";
    
    /// <summary>
    /// The role claim type constant
    /// </summary>
    public const string RoleClaimType = "role";
    
    /// <summary>
    /// The scope constant
    /// </summary>
    public const string Scope = "mango";

    /// <summary>
    /// The access token.
    /// </summary>
    public const string AccessToken = "access_token";
    
    /// <summary>
    /// The api type.
    /// </summary>
    public enum ApiType
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}