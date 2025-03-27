using NetPay.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using static NetPay.Data.Constraints.EntityConstraints.Household;

namespace NetPay.DataProcessor.ImportDtos;

[XmlType(nameof(Household))]
public class ImportHouseholdXmlDto
{
    [Required]
    [MinLength(ContactPersonMinLength), MaxLength(ContactPersonMaxLength)]
    [XmlElement(nameof(ContactPerson))]
    public string ContactPerson { get; set; } = null!;

    [MinLength(EmailMinLength), MaxLength(EmailMaxLength)]
    [XmlElement(nameof(Email))]
    public string? Email { get; set; }

    [Required]
    [MaxLength(PhoneNumberLength)]
    [RegularExpression(PhoneNumberRegex)]
    [XmlAttribute("phone")]
    public string PhoneNumber { get; set; } = null!;
}