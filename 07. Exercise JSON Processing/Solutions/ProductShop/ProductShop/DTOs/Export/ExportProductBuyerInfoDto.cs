namespace ProductShop.DTOs.Export;

public class ExportProductBuyerInfoDto
{
    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string? BuyerFirstName { get; set; }

    public string? BuyerLastName { get; set; }
}