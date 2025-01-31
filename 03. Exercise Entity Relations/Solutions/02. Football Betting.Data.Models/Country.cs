using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models;

public class Country
{
    [Key]
    public int CountryId { get; set; }

    [Required]
    [Column(TypeName = "NVARCHAR(120)")]
    public string Name { get; set; } = null!;

    public ICollection<Town> Towns { get; set; } = new HashSet<Town>();
}