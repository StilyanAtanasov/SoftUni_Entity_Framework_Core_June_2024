using System.Xml.Serialization;

namespace ProductShop.DTOs.Export;

[XmlType("User")]
public class ExportUsersSoldProductsDto
{
    public ExportUsersSoldProductsDto()
    {
        SoldProducts = new HashSet<ExportProductDto>();
    }

    [XmlElement("firstName")]
    public string? FirstName { get; set; }

    [XmlElement("lastName")]
    public string LastName { get; set; } = null!;

    [XmlArray("soldProducts")]
    public HashSet<ExportProductDto> SoldProducts { get; set; }
}