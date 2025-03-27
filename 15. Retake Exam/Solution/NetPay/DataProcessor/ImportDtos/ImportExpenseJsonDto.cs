using System.ComponentModel.DataAnnotations;
using static NetPay.Data.Constraints.EntityConstraints.Expense;

namespace NetPay.DataProcessor.ImportDtos;

public class ImportExpenseJsonDto
{
    [Required]
    [MinLength(ExpenseNameMinLength), MaxLength(ExpenseNameMaxLength)]
    public string ExpenseName { get; set; } = null!;

    [Required]
    [Range(typeof(decimal), AmountMinValue, AmountMaxValue)]
    public decimal Amount { get; set; }

    [Required]
    public string DueDate { get; set; } = null!;

    [Required]
    public string PaymentStatus { get; set; } = null!;

    [Required]
    public int HouseholdId { get; set; }

    [Required]
    public int ServiceId { get; set; }
}