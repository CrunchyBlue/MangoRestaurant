using System.Net.Http.Headers;
using System.Text;
using Mango.Web.Constants;
using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Newtonsoft.Json;
using static System.GC;

namespace Mango.Web.Services;

/// <summary>
/// The base service.
/// </summary>
public class BaseService : IBaseService
{
    /// <inheritdoc cref="IBaseService.Response"/>
    public ResponseDto Response { get; set; }

    /// <summary>
    /// The http client factory.
    /// </summary>
    public IHttpClientFactory httpClientFactory { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseService"/> class.
    /// </summary>
    /// <param name="httpClientFactory"></param>
    public BaseService(IHttpClientFactory httpClientFactory)
    {
        Response = new ResponseDto();
        this.httpClientFactory = httpClientFactory;
    }

    /// <inheritdoc cref="IBaseService.SendAsync" />
    public async Task<T> SendAsync<T>(Request request)
    {
        try
        {
            var client = httpClientFactory.CreateClient("MangoAPI");
            var message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri(request.Url);
            client.DefaultRequestHeaders.Clear();
            if (request.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8,
                    "application/json");
            }

            if (!string.IsNullOrWhiteSpace(request.AccessToken))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", request.AccessToken);
            }

            HttpResponseMessage response = null;

            message.Method = request.ApiType switch
            {
                SD.ApiType.POST => HttpMethod.Post,
                SD.ApiType.PUT => HttpMethod.Put,
                SD.ApiType.DELETE => HttpMethod.Delete,
                _ => HttpMethod.Get
            };

            response = await client.SendAsync(message);

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
        catch (Exception e)
        {
            var dto = new ResponseDto()
            {
                DisplayMessage = "Error",
                ErrorMessages = new List<string>
                {
                    Convert.ToString(e.Message)
                },
                IsSuccess = false
            };
            var res = JsonConvert.SerializeObject(dto);
            return JsonConvert.DeserializeObject<T>(res);
        }
    }

    /// <summary>
    /// The dispose.
    /// </summary>
    public void Dispose()
    {
        SuppressFinalize(true);
    }
}