using System.Text.Json.Serialization;

namespace BlazorWasmTemplate.Shared.ViewModels;

public class BaseVm
{
    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; }
    [JsonPropertyName("createdByName")]
    public string? CreatedByName { get; set; }
    [JsonPropertyName("lastUpdatedAt")]
    public DateTime? LastUpdatedAt { get; set; }
    [JsonPropertyName("lastUpdatedByName")]
    public string? LastUpdatedByName { get; set; }

}
