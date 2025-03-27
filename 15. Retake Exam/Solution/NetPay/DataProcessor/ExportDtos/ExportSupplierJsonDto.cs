using System.Text.Json.Serialization;

namespace NetPay.DataProcessor.ExportDtos;

public class ExportSupplierJsonDto
{
    [JsonPropertyName(nameof(SupplierName))]
    public string SupplierName { get; set; } = null!;
}