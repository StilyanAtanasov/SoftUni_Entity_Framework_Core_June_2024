using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_SalesDatabase.Data.Models;

public class Store
{
    [Key]
    public int StoreId { get; set; }

    [Required]
    [Column(TypeName = "NVARCHAR(80)")]
    public string Name { get; set; } = null!;

    public ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();
}