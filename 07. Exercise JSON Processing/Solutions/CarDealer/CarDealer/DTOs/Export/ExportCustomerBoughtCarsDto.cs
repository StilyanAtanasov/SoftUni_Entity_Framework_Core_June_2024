namespace CarDealer.DTOs.Export;

public class ExportCustomerBoughtCarsDto
{
    public string FullName { get; set; } = null!;

    public int BoughtCars { get; set; }

    public decimal SpentMoney { get; set; }
}