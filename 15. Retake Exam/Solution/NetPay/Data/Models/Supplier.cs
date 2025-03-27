using System.ComponentModel.DataAnnotations;
using static NetPay.Data.Constraints.EntityConstraints.Supplier;

namespace NetPay.Data.Models;

public class Supplier
{
    public Supplier() => SuppliersServices = new HashSet<SupplierService>();

    public int Id { get; set; }

    [Required]
    [MaxLength(SupplierNameMaxLength)]
    public string SupplierName { get; set; } = null!;

    public ICollection<SupplierService> SuppliersServices { get; set; }
}