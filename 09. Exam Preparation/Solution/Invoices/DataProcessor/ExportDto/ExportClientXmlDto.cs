using Invoices.Data.Models;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ExportDto;

[XmlType(nameof(Client))]
public class ExportClientXmlDto
{
    public ExportClientXmlDto() => Invoices = new();

    [XmlElement(nameof(ClientName))]
    public string ClientName { get; set; } = null!;

    [XmlElement(nameof(VatNumber))]
    public string VatNumber { get; set; } = null!;

    [XmlArray(nameof(Invoices))]
    public HashSet<ExportInvoiceXmlDto> Invoices { get; set; }

    [XmlAttribute(nameof(InvoicesCount))]
    public int InvoicesCount { get; set; }
}