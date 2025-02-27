using System.Text.Json.Serialization;

namespace Boardgames.DataProcessor.ExportDto;

public class ExportBoardgameJsonDto
{
    [JsonPropertyName(nameof(Name))]
    public string Name { get; set; } = null!;

    [JsonPropertyName(nameof(Rating))]
    public double Rating { get; set; }

    [JsonPropertyName(nameof(Mechanics))]
    public string Mechanics { get; set; } = null!;

    [JsonPropertyName(nameof(Category))]
    public string Category { get; set; } = null!;
}