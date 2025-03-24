using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using TravelAgency.Data;
using TravelAgency.Data.Models;
using TravelAgency.DataProcessor.ImportDtos;

namespace TravelAgency.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data format!";
        private const string DuplicationDataMessage = "Error! Data duplicated.";
        private const string SuccessfullyImportedCustomer = "Successfully imported customer - {0}";
        private const string SuccessfullyImportedBooking = "Successfully imported booking. TourPackage: {0}, Date: {1}";

        public static string ImportCustomers(TravelAgencyContext context, string xmlString)
        {
            ImportCustomerXmlDto[] customerDtos = DeserializeXml<ImportCustomerXmlDto[]>(xmlString, "Customers");

            StringBuilder sb = new();
            ICollection<Customer> customers = new HashSet<Customer>();

            foreach (ImportCustomerXmlDto customerDto in customerDtos)
            {
                if (!IsValid(customerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isDuplicateInContext = context.Customers
                    .Any(c => c.FullName == customerDto.FullName
                              || c.Email == customerDto.Email
                              || c.PhoneNumber == customerDto.PhoneNumber);

                bool isDuplicateInDataSet = customers
                    .Any(c => c.FullName == customerDto.FullName
                              || c.Email == customerDto.Email
                              || c.PhoneNumber == customerDto.PhoneNumber);

                if (isDuplicateInDataSet || isDuplicateInContext)
                {
                    sb.AppendLine(DuplicationDataMessage);
                    continue;
                }

                Customer customer = new()
                {
                    FullName = customerDto.FullName,
                    Email = customerDto.Email,
                    PhoneNumber = customerDto.PhoneNumber
                };

                customers.Add(customer);
                sb.AppendLine(string.Format(SuccessfullyImportedCustomer, customer.FullName));
            }

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportBookings(TravelAgencyContext context, string jsonString)
        {
            ImportBookingJsonDto[] bookingsJson = DeserializeJson<ImportBookingJsonDto[]>(jsonString);

            StringBuilder sb = new();
            ICollection<Booking> bookings = new HashSet<Booking>();

            foreach (ImportBookingJsonDto bookingDto in bookingsJson)
            {
                if (!IsValid(bookingDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isDateValid = DateTime.TryParseExact(bookingDto.BookingDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal,
                    out DateTime date);
                Customer? customer = context.Customers.FirstOrDefault(c => c.FullName == bookingDto.CustomerName);
                TourPackage? tourPackage = context.TourPackages.FirstOrDefault(tp => tp.PackageName == bookingDto.TourPackageName);

                if (!isDateValid || customer == null || tourPackage == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Booking booking = new()
                {
                    BookingDate = date,
                    Customer = customer,
                    TourPackage = tourPackage
                };

                bookings.Add(booking);
                sb.AppendLine(string.Format(SuccessfullyImportedBooking, booking.TourPackage.PackageName,
                    booking.BookingDate.ToString("yyyy-MM-dd")));
            }

            context.Bookings.AddRange(bookings);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static bool IsValid(object dto)
        {
            var validateContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(dto, validateContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                string currValidationMessage = validationResult.ErrorMessage;
            }

            return isValid;
        }

        private static T DeserializeXml<T>(string xml, string rootElementName)
        {
            XmlSerializer serializer = new(typeof(T), new XmlRootAttribute(rootElementName));
            using StringReader reader = new(xml);

            return (T)serializer.Deserialize(reader)!;
        }

        private static T DeserializeJson<T>(string json) => JsonSerializer.Deserialize<T>(json)!;
    }
}
