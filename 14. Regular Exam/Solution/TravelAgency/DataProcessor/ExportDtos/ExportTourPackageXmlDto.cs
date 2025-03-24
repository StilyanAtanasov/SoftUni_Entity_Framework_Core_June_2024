using System.Xml.Serialization;
using TravelAgency.Data.Models;

namespace TravelAgency.DataProcessor.ExportDtos;

[XmlType(nameof(TourPackage))]
public class ExportTourPackageXmlDto
{
    [XmlElement(nameof(Name))]
    public string Name { get; set; } = null!;

    [XmlElement(nameof(Description))]
    public string? Description { get; set; }

    [XmlElement(nameof(Price))]
    public decimal Price { get; set; }
}