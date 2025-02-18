namespace ProductShop.DTOs.Export;

public class UserSellers
{
    public string? FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int? Age { get; set; }

    public ProductsDto SoldProducts { get; set; } = null!;
}