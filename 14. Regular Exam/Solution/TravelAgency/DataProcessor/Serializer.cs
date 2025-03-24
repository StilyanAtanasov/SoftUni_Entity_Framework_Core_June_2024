using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Xml.Serialization;
using TravelAgency.Data;
using TravelAgency.Data.Models.Enums;
using TravelAgency.DataProcessor.ExportDtos;

namespace TravelAgency.DataProcessor
{
    public class Serializer
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        public static string ExportGuidesWithSpanishLanguageWithAllTheirTourPackages(TravelAgencyContext context)
        {
            ExportGuideXmlDto[] guides = context.Guides
                .Where(g => g.Language == Language.Spanish)
                .Select(g => new ExportGuideXmlDto
                {
                    FullName = g.FullName,
                    TourPackages = g.TourPackagesGuides
                        .Select(tpg => new ExportTourPackageXmlDto
                        {
                            Name = tpg.TourPackage.PackageName,
                            Description = tpg.TourPackage.Description,
                            Price = tpg.TourPackage.Price
                        })
                        .OrderByDescending(tp => tp.Price)
                        .ThenBy(tp => tp.Name)
                        .ToArray()
                })
                .OrderByDescending(g => g.TourPackages.Length)
                .ThenBy(g => g.FullName)
                .ToArray();

            return SerializeXml(guides, "Guides");
        }

        public static string ExportCustomersThatHaveBookedHorseRidingTourPackage(TravelAgencyContext context)
        {
            var customers = context.Customers
                 .Where(c => c.Bookings.Any(b => b.TourPackage.PackageName == "Horse Riding Tour"))
                 .Select(c => new
                 {
                     c.FullName,
                     c.PhoneNumber,
                     Bookings = c.Bookings
                         .Where(b => b.TourPackage.PackageName == "Horse Riding Tour")
                         .Select(b => new
                         {
                             TourPackageName = b.TourPackage.PackageName,
                             Date = b.BookingDate
                         })
                         .OrderBy(b => b.Date)
                         .ToArray()
                 })
                 .OrderByDescending(c => c.Bookings.Length)
                 .ThenBy(c => c.FullName)
                 .ToArray();

            ExportCustomerJsonDto[] customersDto = customers
                .Select(c => new ExportCustomerJsonDto
                {
                    FullName = c.FullName,
                    PhoneNumber = c.PhoneNumber,
                    Bookings = c.Bookings
                        .Select(b => new ExportBookingJsonDto
                        {
                            TourPackageName = b.TourPackageName,
                            Date = b.Date.ToString("yyyy-MM-dd")
                        })
                        .OrderBy(b => b.Date)
                        .ToArray()
                })
                .OrderByDescending(c => c.Bookings.Length)
                .ThenBy(c => c.FullName)
                .ToArray();

            return SerializeJson(customersDto);
        }

        private static string SerializeXml<T>(T dto, string rootElementName)
        {
            XmlSerializer serializer = new(typeof(T), new XmlRootAttribute(rootElementName));

            XmlSerializerNamespaces xmlns = new();
            xmlns.Add(string.Empty, string.Empty);

            using StringWriter writer = new(new StringBuilder());

            serializer.Serialize(writer, dto, xmlns);
            return writer.ToString().Trim();
        }

        private static string SerializeJson<T>(T dto) => JsonSerializer.Serialize(dto, JsonSerializerOptions);
    }
}
