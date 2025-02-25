using Invoices.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using static Invoices.Data.DataConstraints;

namespace Invoices.DataProcessor.ImportDto;

[XmlType(nameof(Address))]
public class ImportAddressXmlDto
{
    [Required]
    [MinLength(AddressStreetNameMinLength)]
    [MaxLength(AddressStreetNameMaxLength)]
    [XmlElement(nameof(StreetName))]
    public string StreetName { get; set; } = null!;

    [Required]
    [XmlElement(nameof(StreetNumber))]
    public int StreetNumber { get; set; }

    [Required]
    [XmlElement(nameof(PostCode))]
    public string PostCode { get; set; } = null!;

    [Required]
    [MinLength(AddressCityMinLength)]
    [MaxLength(AddressCityMaxLength)]
    [XmlElement(nameof(City))]
    public string City { get; set; } = null!;

    [Required]
    [MinLength(AddressCountryMinLength)]
    [MaxLength(AddressCountryMaxLength)]
    [XmlElement(nameof(Country))]
    public string Country { get; set; } = null!;
}