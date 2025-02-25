namespace Invoices.DataProcessor.ExportDto;

public class ExportProductsJsonDto
{
    public ExportProductsJsonDto() => Clients = new();

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string Category { get; set; } = null!;

    public HashSet<ExportClientJsonDto> Clients { get; set; }
}