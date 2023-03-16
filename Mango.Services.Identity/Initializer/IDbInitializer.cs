namespace Mango.Services.Identity.Initializer;

/// <summary>
/// The db initializer interface.
/// </summary>
public interface IDbInitializer
{
    /// <summary>
    /// The initialize.
    /// </summary>
    public Task Initialize();
}