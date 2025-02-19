using System.Xml.Serialization;

namespace ProductShop.DTOs.Export;

[XmlType("User")]
public class UserSellers
{
    [XmlElement("firstName")]
    public string? FirstName { get; set; }

    [XmlElement("lastName")]
    public string LastName { get; set; } = null!;

    [XmlElement("age")]
    public int? Age { get; set; }

    public ProductsDto SoldProducts { get; set; } = null!;
}