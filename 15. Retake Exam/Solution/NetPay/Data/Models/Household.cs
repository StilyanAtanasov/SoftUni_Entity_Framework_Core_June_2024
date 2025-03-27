using System.ComponentModel.DataAnnotations;
using static NetPay.Data.Constraints.EntityConstraints.Household;

namespace NetPay.Data.Models;

public class Household
{
    public Household() => Expenses = new HashSet<Expense>();

    public int Id { get; set; }

    [Required]
    [MaxLength(ContactPersonMaxLength)]
    public string ContactPerson { get; set; } = null!;

    [MaxLength(EmailMaxLength)]
    public string? Email { get; set; }

    [Required]
    [MaxLength(PhoneNumberLength)]
    public string PhoneNumber { get; set; } = null!;

    public ICollection<Expense> Expenses { get; set; }
}