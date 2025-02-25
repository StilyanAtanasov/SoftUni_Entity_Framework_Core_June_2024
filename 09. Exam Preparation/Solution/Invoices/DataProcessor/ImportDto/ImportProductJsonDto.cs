using Invoices.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using static Invoices.Data.DataConstraints;

namespace Invoices.DataProcessor.ImportDto;

public class ImportProductJsonDto
{
    public ImportProductJsonDto() => Clients = new HashSet<int>();

    [Required]
    [MinLength(ProductNameMinLength)]
    [MaxLength(ProductNameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [Range(typeof(decimal), ProductMinPrice, ProductMaxPrice)]
    public decimal Price { get; set; }

    [Required]
    [Range(ProductMinCategoryType, ProductMaxCategoryType)]
    public int CategoryType { get; set; }

    public HashSet<int> Clients { get; set; }
}