using System.Globalization;
using System.Text.Json.Serialization;

namespace CarDealer.DTOs.Export;

public class ExportCustomerDto
{
    public string Name { get; set; } = null!;

    [JsonIgnore]
    public DateTime BirthDate { get; set; }

    [JsonPropertyName("BirthDate")]
    public string BirthDateFormatted => BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

    public bool IsYoungDriver { get; set; }
}