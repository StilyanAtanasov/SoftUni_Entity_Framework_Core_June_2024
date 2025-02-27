using System.Text.Json.Serialization;

namespace Boardgames.DataProcessor.ExportDto;

public class ExportSellerJsonDto
{
    [JsonPropertyName(nameof(Name))]
    public string Name { get; set; } = null!;

    [JsonPropertyName(nameof(Website))]
    public string Website { get; set; } = null!;

    [JsonPropertyName(nameof(Boardgames))]
    public ExportBoardgameJsonDto[] Boardgames { get; set; } = null!;
}