using Invoices.Data.Models;
using Invoices.Data.Models.Enums;
using Invoices.DataProcessor.ImportDto;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace Invoices.DataProcessor;

using Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Deserializer
{
    private const string ErrorMessage = "Invalid data!";

    private const string SuccessfullyImportedClients
        = "Successfully imported client {0}.";

    private const string SuccessfullyImportedInvoices
        = "Successfully imported invoice with number {0}.";

    private const string SuccessfullyImportedProducts
        = "Successfully imported product - {0} with {1} clients.";


    public static string ImportClients(InvoicesContext context, string xmlString)
    {
        ImportClientXmlDto[] clientDtos = DeserializeXml<ImportClientXmlDto[]>(xmlString, "Clients");
        StringBuilder sb = new();

        HashSet<Client> clients = new();

        foreach (ImportClientXmlDto c in clientDtos)
        {
            if (!IsValid(c))
            {
                sb.AppendLine(ErrorMessage);
                continue;
            }

            HashSet<Address> addresses = new();
            foreach (ImportAddressXmlDto a in c.Addresses)
            {
                if (!IsValid(a))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                addresses.Add(new Address
                {
                    StreetName = a.StreetName,
                    StreetNumber = a.StreetNumber,
                    PostCode = a.PostCode,
                    City = a.City,
                    Country = a.Country
                });
            }

            clients.Add(new Client
            {
                Name = c.Name,
                NumberVat = c.NumberVat,
                Addresses = addresses
            });

            sb.AppendLine(string.Format(SuccessfullyImportedClients, c.Name));
        }

        context.Clients.AddRange(clients);
        context.SaveChanges();

        return sb.ToString().Trim();
    }

    public static string ImportInvoices(InvoicesContext context, string jsonString)
    {
        ImportInvoiceJsonDto[] invoiceDtos = DeserializeJson<ImportInvoiceJsonDto[]>(jsonString);
        StringBuilder sb = new();
        HashSet<Invoice> invoices = new();

        foreach (ImportInvoiceJsonDto i in invoiceDtos)
        {
            bool isDueDateValid = DateTime.TryParse(i.DueDate, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out DateTime dueDate);
            bool isIssueDateValid = DateTime.TryParse(i.IssueDate, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out DateTime issueDate);

            if (!IsValid(i) || !context.Clients.Any(c => c.Id == i.ClientId) || !isDueDateValid || !isIssueDateValid ||
                DateTime.Compare(dueDate, issueDate) < 0)
            {
                sb.AppendLine(ErrorMessage);
                continue;
            }

            invoices.Add(new Invoice
            {
                Number = i.Number,
                Amount = i.Amount,
                DueDate = dueDate,
                IssueDate = issueDate,
                CurrencyType = (CurrencyType)i.CurrencyType,
                ClientId = i.ClientId,
            });

            sb.AppendLine(string.Format(SuccessfullyImportedInvoices, i.Number));
        }

        context.Invoices.AddRange(invoices);
        context.SaveChanges();

        return sb.ToString().Trim();
    }

    public static string ImportProducts(InvoicesContext context, string jsonString)
    {
        ImportProductJsonDto[] productDtos = DeserializeJson<ImportProductJsonDto[]>(jsonString);
        StringBuilder sb = new();
        HashSet<Product> products = new();

        foreach (ImportProductJsonDto p in productDtos)
        {
            if (!IsValid(p))
            {
                sb.AppendLine(ErrorMessage);
                continue;
            }

            HashSet<int> realClientIds = new();
            foreach (int ci in p.Clients)
            {
                if (!context.Clients.Any(c => c.Id == ci))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                realClientIds.Add(ci);
            }

            products.Add(new Product
            {
                Name = p.Name,
                Price = p.Price,
                CategoryType = (CategoryType)p.CategoryType,
                ProductsClients = realClientIds
                    .Select(ci => new ProductClient { ClientId = ci })
                    .ToArray()
            });

            sb.AppendLine(string.Format(SuccessfullyImportedProducts, p.Name, realClientIds.Count));
        }

        context.Products.AddRange(products);
        context.SaveChanges();

        return sb.ToString().Trim();
    }

    public static bool IsValid(object dto)
    {
        var validationContext = new ValidationContext(dto);
        var validationResult = new List<ValidationResult>();

        return Validator.TryValidateObject(dto, validationContext, validationResult, true);
    }

    private static T DeserializeXml<T>(string xml, string rootAttributeName) where T : class
    {
        XmlSerializer serializer = new(typeof(T), new XmlRootAttribute(rootAttributeName));
        using StringReader reader = new(xml);

        return (T)serializer.Deserialize(reader)!;
    }

    private static T DeserializeJson<T>(string json) where T : class => JsonSerializer.Deserialize<T>(json)!;
}