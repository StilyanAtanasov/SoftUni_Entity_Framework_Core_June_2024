using Invoices.DataProcessor.ExportDto;
using System.Globalization;
using System.Text.Json;
using System.Xml.Serialization;

namespace Invoices.DataProcessor;

using Data;

public class Serializer
{
    private static readonly JsonSerializerOptions JsonExportOptions = new()
    {
        WriteIndented = true
    };

    public static string ExportClientsWithTheirInvoices(InvoicesContext context, DateTime date)
    {
        ExportClientXmlDto[] clients = context.Clients
            .AsEnumerable()
            .Where(c => c.Invoices.Any(i => DateTime.Compare(i.IssueDate, date) > 0))
            .Select(c => new ExportClientXmlDto
            {
                ClientName = c.Name,
                VatNumber = c.NumberVat,
                InvoicesCount = c.Invoices.Count,
                Invoices = c.Invoices
                    .OrderBy(i => i.IssueDate)
                    .ThenByDescending(i => i.DueDate)
                    .Select(i => new ExportInvoiceXmlDto
                    {
                        InvoiceNumber = i.Number,
                        InvoiceAmount = i.Amount,
                        DueDate = i.DueDate.ToString("d", CultureInfo.InvariantCulture),
                        Currency = i.CurrencyType.ToString()
                    })
                    .ToHashSet()
            })
            .OrderByDescending(c => c.InvoicesCount)
            .ThenBy(c => c.ClientName)
            .ToArray();

        return SerializeXml(clients, "Clients");
    }

    public static string ExportProductsWithMostClients(InvoicesContext context, int nameLength)
    {
        ExportProductsJsonDto[] products = context.Products
            .AsEnumerable()
            .Where(p => p.ProductsClients.Any(pc => pc.Client.Name.Length >= nameLength))
            .Select(p => new ExportProductsJsonDto
            {
                Name = p.Name,
                Price = p.Price,
                Category = p.CategoryType.ToString(),
                Clients = p.ProductsClients
                    .Where(pc => pc.Client.Name.Length >= nameLength)
                    .Select(pc => new ExportClientJsonDto
                    {
                        Name = pc.Client.Name,
                        NumberVat = pc.Client.NumberVat,
                    })
                    .OrderBy(c => c.Name)
                    .ToHashSet()
            })
            .OrderByDescending(p => p.Clients.Count)
            .ThenBy(p => p.Name)
            .Take(5)
            .ToArray();

        return SerializeJson(products);
    }

    private static string SerializeJson<T>(T dto) => JsonSerializer.Serialize(dto, JsonExportOptions);

    private static string SerializeXml<T>(T dto, string rootElementName)
    {
        XmlSerializerNamespaces xmlns = new();
        xmlns.Add("", "");

        XmlSerializer serializer = new(typeof(T), new XmlRootAttribute(rootElementName));
        using StringWriter sw = new();

        serializer.Serialize(sw, dto, xmlns);
        return sw.ToString();
    }
}