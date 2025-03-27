using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetPay.Data.Models;

public class SupplierService
{
    [Required]
    [ForeignKey(nameof(Supplier))]
    public int SupplierId { get; set; }

    public Supplier Supplier { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Service))]
    public int ServiceId { get; set; }

    public Service Service { get; set; } = null!;
}