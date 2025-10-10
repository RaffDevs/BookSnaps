using System.Text.Json.Serialization;

namespace BookSnaps.Infra.Services.DTOs;

public class BrasilApiResult
{
    [JsonPropertyName("isbn")]
    public string? Isbn { get; set; } = string.Empty;
    
    [JsonPropertyName("title")]
    public string? Title { get; set; } = string.Empty;
    
    [JsonPropertyName("subtitle")]
    public string? Subtitle { get; set; } = string.Empty;
    
    [JsonPropertyName("authors")]
    public IEnumerable<string> Authors { get; set; } = new List<string>();
    
    [JsonPropertyName("publisher")]
    public string? Publisher { get; set; } = string.Empty;
    
    [JsonPropertyName("synopsis")]
    public string? Synopsis { get; set; } = string.Empty;

    [JsonPropertyName("page_count")] public int? PageCount { get; set; } = 0;
    
}