using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.Wrapper;

public class ResultApi<T> where T : class
{
    public ResultApi()
    {
        Success = true;
        Code = 200;
        Message = "OK";
        InnerMessage = null;
        Payload = default!;
    }
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    [JsonPropertyName("Code")]
    public int Code { get; set; }
    [JsonPropertyName("message")]
    public string? Message { get; set; }
    [JsonPropertyName("innerMessage")]
    public string? InnerMessage { get; set; }
    [JsonPropertyName("payload")]
    public T Payload { get; set; }

    public static ResultApi<T> Deserialize(string json)
    {
        ResultApi<T> result = JsonSerializer.Deserialize<ResultApi<T>>(json)!;

        if (!result.Success)
        {
            result.Code = result.Code;
            result.Message ??= "Request failed";
        }

        return result;
    }
}

