using System.Text.Json.Serialization;

namespace CarDealer.DTOs.Export;

public class ExportCarPartsDto
{
    public ExportCarPartsDto()
    {
        Parts = new HashSet<ExportPartDto>();
    }

    [JsonPropertyName("car")]
    public ExportCarWithoutIdDto Car { get; set; } = null!;

    [JsonPropertyName("parts")]
    public ICollection<ExportPartDto> Parts { get; set; }
}