using System.Text.Json.Serialization;

namespace NetPay.DataProcessor.ExportDtos;

public class ExportServiceJsonDto
{
    [JsonPropertyName(nameof(ServiceName))]
    public string ServiceName { get; set; } = null!;

    [JsonPropertyName(nameof(Suppliers))]
    public ExportSupplierJsonDto[] Suppliers { get; set; } = null!;
}