using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_SalesDatabase.Data.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required]
    [Column(TypeName = "NVARCHAR(50)")]
    public string Name { get; set; } = null!;

    [Required]
    public double Quantity { get; set; }

    [Required]
    [Column(TypeName = "DECIMAL(18,2)")]
    public decimal Price { get; set; }

    public ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();
}