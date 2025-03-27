using NetPay.Data;
using NetPay.Data.Models;
using NetPay.Data.Models.Enums;
using NetPay.DataProcessor.ImportDtos;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace NetPay.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data format!";
        private const string DuplicationDataMessage = "Error! Data duplicated.";
        private const string SuccessfullyImportedHousehold = "Successfully imported household. Contact person: {0}";
        private const string SuccessfullyImportedExpense = "Successfully imported expense. {0}, Amount: {1}";

        public static string ImportHouseholds(NetPayContext context, string xmlString)
        {
            ImportHouseholdXmlDto[] householdDtos = DeserializeXml<ImportHouseholdXmlDto[]>(xmlString, "Households");

            StringBuilder sb = new();
            ICollection<Household> validHouseholds = new HashSet<Household>();

            foreach (ImportHouseholdXmlDto household in householdDtos)
            {
                if (!IsValid(household))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool hasDuplicateInContext = context.Households
                        .Any(h =>
                            h.ContactPerson == household.ContactPerson
                            || h.Email == household.Email
                            || h.PhoneNumber == household.PhoneNumber);

                bool hasDuplicateInBatch = validHouseholds
                    .Any(h =>
                        h.ContactPerson == household.ContactPerson
                        || h.Email == household.Email
                        || h.PhoneNumber == household.PhoneNumber);

                if (hasDuplicateInBatch || hasDuplicateInContext)
                {
                    sb.AppendLine(DuplicationDataMessage);
                    continue;
                }

                validHouseholds.Add(new()
                {
                    ContactPerson = household.ContactPerson,
                    Email = household.Email,
                    PhoneNumber = household.PhoneNumber,
                });
                sb.AppendLine(string.Format(SuccessfullyImportedHousehold, household.ContactPerson));
            }

            context.Households.AddRange(validHouseholds);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportExpenses(NetPayContext context, string jsonString)
        {
            ImportExpenseJsonDto[] expenseDtos = DeserializeJson<ImportExpenseJsonDto[]>(jsonString);

            StringBuilder sb = new();
            ICollection<Expense> validExpenses = new HashSet<Expense>();

            foreach (ImportExpenseJsonDto expense in expenseDtos)
            {
                if (!IsValid(expense))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isDueDateValid = DateTime.TryParseExact(expense.DueDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dueDate);
                bool isPaymentStatusValid = Enum.TryParse(expense.PaymentStatus, out PaymentStatus paymentStatus);
                bool isHouseholdValid = context.Households.Any(h => h.Id == expense.HouseholdId);
                bool isServiceValid = context.Services.Any(s => s.Id == expense.ServiceId);

                if (!isPaymentStatusValid || !isHouseholdValid || !isServiceValid || !isDueDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                validExpenses.Add(new()
                {
                    ExpenseName = expense.ExpenseName,
                    Amount = expense.Amount,
                    DueDate = dueDate,
                    PaymentStatus = paymentStatus,
                    HouseholdId = expense.HouseholdId,
                    ServiceId = expense.ServiceId
                });
                sb.AppendLine(string.Format(SuccessfullyImportedExpense, expense.ExpenseName, $"{expense.Amount:F2}"));
            }

            context.Expenses.AddRange(validExpenses);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            return isValid;
        }

        private static T DeserializeXml<T>(string xmlString, string xmlRootAttributeName)
        {
            XmlSerializer serializer = new(typeof(T), new XmlRootAttribute(xmlRootAttributeName));
            using StringReader reader = new(xmlString);

            return (T)serializer.Deserialize(reader)!;
        }

        private static T DeserializeJson<T>(string jsonString) => JsonSerializer.Deserialize<T>(jsonString)!;
    }
}
