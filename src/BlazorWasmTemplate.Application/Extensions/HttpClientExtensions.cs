using System.Text;
using System.Text.Json;


namespace BlazorWasmTemplate.Application.Extensions;

public static class HttpClientExtensions
{
    public static async Task<HttpResponseMessage> GetAsync(this IHttpClientFactory client, string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        return await client.CreateClient("komodoWaterApiV1").SendAsync(request);
    }

    public static async Task<HttpResponseMessage> PostAsync(this IHttpClientFactory client, string url, object data)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json")
        };

        return await client.CreateClient("komodoWaterApiV1").SendAsync(request);
    }

    public static async Task<HttpResponseMessage> PutAsync(this IHttpClientFactory client, string url, object data)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, url)
        {
            Content = new StringContent(JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json")
        };

        return await client.CreateClient("komodoWaterApiV1").SendAsync(request);
    }

    public static async Task<HttpResponseMessage> DeleteAsync(this IHttpClientFactory client, string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, url);

        return await client.CreateClient("komodoWaterApiV1").SendAsync(request);
    }
}
