using System.Xml.Serialization;

namespace ProductShop.DTOs.Export;

[XmlType("Category")]
public class ExportCategoryByProductsDto
{
    [XmlElement("name")]
    public string Category { get; set; } = null!;

    [XmlElement("count")]
    public int ProductsCount { get; set; }

    [XmlElement("averagePrice")]
    public decimal AveragePrice { get; set; }

    [XmlElement("totalRevenue")]
    public decimal TotalRevenue { get; set; }
}