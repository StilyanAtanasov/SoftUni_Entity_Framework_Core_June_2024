using System.ComponentModel.DataAnnotations;
using static NetPay.Data.Constraints.EntityConstraints.Service;

namespace NetPay.Data.Models;

public class Service
{
    public Service()
    {
        Expenses = new HashSet<Expense>();
        SuppliersServices = new HashSet<SupplierService>();
    }

    public int Id { get; set; }

    [Required]
    [MaxLength(ServiceNameMaxLength)]
    public string ServiceName { get; set; } = null!;

    public ICollection<Expense> Expenses { get; set; }

    public ICollection<SupplierService> SuppliersServices { get; set; }
}