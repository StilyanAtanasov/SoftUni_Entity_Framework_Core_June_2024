namespace ProductShop.DTOs.Export;

public class ProductsDto
{
    public ProductsDto()
    {
        Products = new HashSet<ProductDto>();
    }

    public int Count { get; set; }
    public ICollection<ProductDto> Products { get; set; }
}