using Invoices.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using static Invoices.Data.DataConstraints;

namespace Invoices.Data.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(ProductNameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    public decimal Price { get; set; }

    [Required]
    public CategoryType CategoryType { get; set; }

    public ICollection<ProductClient> ProductsClients { get; set; } = new HashSet<ProductClient>();
}