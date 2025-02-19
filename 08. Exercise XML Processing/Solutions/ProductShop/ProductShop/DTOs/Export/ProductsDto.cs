using System.Xml.Serialization;

namespace ProductShop.DTOs.Export;

public class ProductsDto
{
    public ProductsDto()
    {
        Products = new HashSet<ExportProductDto>();
    }

    [XmlElement("count")]
    public int Count { get; set; }

    [XmlArray("products")]
    public HashSet<ExportProductDto> Products { get; set; }
}