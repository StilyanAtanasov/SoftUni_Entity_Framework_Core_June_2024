namespace ProductShop.DTOs.Export;

public class ExportUsersSoldProductsDto
{
    public ExportUsersSoldProductsDto()
    {
        SoldProducts = new HashSet<ExportProductBuyerInfoDto>();
    }

    public string? FirstName { get; set; }

    public string LastName { get; set; } = null!;

    public ICollection<ExportProductBuyerInfoDto> SoldProducts { get; set; }
}