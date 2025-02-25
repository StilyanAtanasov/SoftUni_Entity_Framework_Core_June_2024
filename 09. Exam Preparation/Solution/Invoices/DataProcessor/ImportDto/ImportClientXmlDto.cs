using Invoices.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using static Invoices.Data.DataConstraints;

namespace Invoices.DataProcessor.ImportDto;

[XmlType(nameof(Client))]
public class ImportClientXmlDto
{
    public ImportClientXmlDto() => Addresses = new HashSet<ImportAddressXmlDto>();

    [Required]
    [MinLength(ClientNameMinLength)]
    [MaxLength(ClientNameMaxLength)]
    [XmlElement(nameof(Name))]
    public string Name { get; set; } = null!;

    [Required]
    [MinLength(ClientNumberVatMinLength)]
    [MaxLength(ClientNumberVatMaxLength)]
    [XmlElement(nameof(NumberVat))]
    public string NumberVat { get; set; } = null!;

    [XmlArray(nameof(Addresses))]
    public HashSet<ImportAddressXmlDto> Addresses { get; set; }
}