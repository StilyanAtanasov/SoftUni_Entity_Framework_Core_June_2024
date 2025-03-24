using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using TravelAgency.Data.Models;
using static TravelAgency.Data.Constraints.EntityConstraints.Customer;

namespace TravelAgency.DataProcessor.ImportDtos;

[XmlType(nameof(Customer))]
public class ImportCustomerXmlDto
{
    [Required]
    [MinLength(FullNameMinLength), MaxLength(FullNameMaxLength)]
    [XmlElement(nameof(FullName))]
    public string FullName { get; set; } = null!;

    [Required]
    [MinLength(EmailMinLength), MaxLength(EmailMaxLength)]
    [XmlElement(nameof(Email))]
    public string Email { get; set; } = null!;

    [Required]
    [RegularExpression(PhoneNumberRegex)]
    [XmlAttribute("phoneNumber")]
    public string PhoneNumber { get; set; } = null!;
}