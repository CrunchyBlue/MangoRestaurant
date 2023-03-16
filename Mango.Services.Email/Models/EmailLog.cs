using System.ComponentModel.DataAnnotations;

namespace Mango.Services.Email.Models;

/// <summary>
/// The email log.
/// </summary>
public class EmailLog
{
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    [Key]
    public int Id { get; set; }
    
    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Gets or sets the log.
    /// </summary>
    public string Log { get; set; }
    
    /// <summary>
    /// Gets or sets the email sent.
    /// </summary>
    public DateTime EmailSent { get; set; }
}