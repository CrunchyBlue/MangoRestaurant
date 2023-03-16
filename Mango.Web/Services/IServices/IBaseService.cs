using Mango.Web.Models;

namespace Mango.Web.Services.IServices;

/// <summary>
/// The base service interface.
/// </summary>
public interface IBaseService : IDisposable
{
    /// <summary>
    /// Gets or sets the response.
    /// </summary>
    public ResponseDto Response { get; set; }
    
    /// <summary>
    /// The send async.
    /// </summary>
    /// <param name="request">
    /// The request.
    /// </param>
    /// <typeparam name="T">
    /// The generic type.
    /// </typeparam>
    /// <returns>
    /// The <see cref="T:Task{T}"/>
    /// </returns>
    public Task<T> SendAsync<T>(Request request);
}