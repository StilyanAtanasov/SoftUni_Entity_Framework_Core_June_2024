using System.Text.Json.Serialization;

namespace TravelAgency.DataProcessor.ExportDtos;

public class ExportCustomerJsonDto
{
    [JsonPropertyName(nameof(FullName))]
    public string FullName { get; set; } = null!;

    [JsonPropertyName(nameof(PhoneNumber))]
    public string PhoneNumber { get; set; } = null!;

    [JsonPropertyName(nameof(Bookings))]
    public ExportBookingJsonDto[] Bookings { get; set; } = null!;
}