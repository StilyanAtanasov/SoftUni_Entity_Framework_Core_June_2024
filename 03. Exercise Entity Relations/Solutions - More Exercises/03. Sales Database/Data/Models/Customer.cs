using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_SalesDatabase.Data.Models;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required]
    [Column(TypeName = "NVARCHAR(100)")]
    public string Name { get; set; } = null!;

    [Required]
    [Column(TypeName = "VARCHAR(80)")]
    public string Email { get; set; } = null!;

    [Required]
    [Column(TypeName = "VARCHAR(70)")]
    public string CreditCardNumber { get; set; } = null!;

    public ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();
}
