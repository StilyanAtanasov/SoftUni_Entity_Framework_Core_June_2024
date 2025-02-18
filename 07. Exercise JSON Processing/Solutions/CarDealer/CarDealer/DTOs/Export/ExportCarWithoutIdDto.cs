using System.Text.Json.Serialization;

namespace CarDealer.DTOs.Export;

public class ExportCarWithoutIdDto
{
    [JsonPropertyName("Make")]
    public string Make { get; set; } = null!;

    [JsonPropertyName("Model")]
    public string Model { get; set; } = null!;

    [JsonPropertyName("TraveledDistance")]
    public long TraveledDistance { get; set; }
}