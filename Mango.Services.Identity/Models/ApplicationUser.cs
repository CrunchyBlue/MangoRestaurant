using Microsoft.AspNetCore.Identity;

namespace Mango.Services.Identity.Models;

/// <summary>
/// The application user.
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    public string LastName { get; set; }
}