using System.Text.Json.Serialization;

namespace TravelAgency.DataProcessor.ExportDtos;

public class ExportBookingJsonDto
{
    [JsonPropertyName(nameof(TourPackageName))]
    public string TourPackageName { get; set; } = null!;

    [JsonPropertyName(nameof(Date))]
    public string Date { get; set; } = null!;
}