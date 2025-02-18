namespace CarDealer.DTOs.Export;

public class ExportSalesDto
{
    public ExportCarWithoutIdDto Car { get; set; } = null!;

    public string CustomerName { get; set; } = null!;

    public string Discount { get; set; } = null!;

    public string Price { get; set; } = null!;

    public string PriceWithDiscount { get; set; } = null!;
}