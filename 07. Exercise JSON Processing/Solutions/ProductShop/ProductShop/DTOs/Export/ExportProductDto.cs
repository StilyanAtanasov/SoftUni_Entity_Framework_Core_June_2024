namespace ProductShop.DTOs.Export;

public class ExportProductDto
{
    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string Seller { get; set; } = null!;
}