using NetPay.Data;
using NetPay.Data.Models.Enums;
using NetPay.DataProcessor.ExportDtos;
using System.Text.Json;
using System.Xml.Serialization;

namespace NetPay.DataProcessor
{
    public class Serializer
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            WriteIndented = true,
        };

        public static string ExportHouseholdsWhichHaveExpensesToPay(NetPayContext context)
        {
            var households = context.Households
                .Where(h => h.Expenses.Any(p => p.PaymentStatus != PaymentStatus.Paid))
                .Select(h => new
                {
                    h.ContactPerson,
                    h.Email,
                    h.PhoneNumber,
                    Expenses = h.Expenses
                        .Where(e => e.PaymentStatus != PaymentStatus.Paid)
                        .Select(e => new
                        {
                            e.ExpenseName,
                            e.Amount,
                            PaymentDate = e.DueDate,
                            e.Service.ServiceName
                        })
                        .OrderBy(e => e.PaymentDate)
                        .ToArray()
                })
                .OrderBy(h => h.ContactPerson)
                .ToArray();

            ExportHouseholdXmlDto[] householdDtos = households
                .Select(h => new ExportHouseholdXmlDto
                {
                    ContactPerson = h.ContactPerson,
                    Email = h.Email,
                    PhoneNumber = h.PhoneNumber,
                    Expenses = h.Expenses
                        .Select(e => new ExportExpenseXmlDto
                        {
                            ExpenseName = e.ExpenseName,
                            Amount = e.Amount.ToString("F2"),
                            PaymentDate = e.PaymentDate.ToString("yyyy-MM-dd"),
                            ServiceName = e.ServiceName
                        })
                        .OrderBy(e => e.PaymentDate)
                        .ThenBy(e => e.Amount)
                        .ToArray()
                })
                .ToArray();

            return SerializeXml(householdDtos, "Households");
        }

        public static string ExportAllServicesWithSuppliers(NetPayContext context)
        {
            ExportServiceJsonDto[] serviceDtos = context.Services
                .Select(s => new ExportServiceJsonDto
                {
                    ServiceName = s.ServiceName,
                    Suppliers = s.SuppliersServices
                        .Select(ss => new ExportSupplierJsonDto
                        {
                            SupplierName = ss.Supplier.SupplierName,
                        })
                        .OrderBy(sup => sup.SupplierName)
                        .ToArray()
                })
                .OrderBy(s => s.ServiceName)
                .ToArray();

            return SerializeJson(serviceDtos);
        }

        private static string SerializeXml<T>(T dto, string xmlRootElementName)
        {
            XmlSerializer serializer = new(typeof(T), new XmlRootAttribute(xmlRootElementName));
            XmlSerializerNamespaces xmlns = new();

            using StringWriter writer = new();

            xmlns.Add("", "");
            serializer.Serialize(writer, dto, xmlns);

            return writer.ToString();
        }

        private static string SerializeJson<T>(T dto) => JsonSerializer.Serialize(dto, JsonSerializerOptions);
    }
}
