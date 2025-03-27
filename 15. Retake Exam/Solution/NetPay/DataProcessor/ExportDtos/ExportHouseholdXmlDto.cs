using NetPay.Data.Models;
using System.Xml.Serialization;

namespace NetPay.DataProcessor.ExportDtos;

[XmlType(nameof(Household))]
public class ExportHouseholdXmlDto
{
    [XmlElement(nameof(ContactPerson))]
    public string ContactPerson { get; set; } = null!;

    [XmlElement(nameof(Email))]
    public string? Email { get; set; }

    [XmlElement(nameof(PhoneNumber))]
    public string PhoneNumber { get; set; } = null!;

    [XmlArray(nameof(Expenses))]
    public ExportExpenseXmlDto[] Expenses { get; set; } = null!;
}