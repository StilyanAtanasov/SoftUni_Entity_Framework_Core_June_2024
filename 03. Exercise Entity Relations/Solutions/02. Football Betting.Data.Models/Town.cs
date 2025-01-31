using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models;

public class Town
{
    [Key]
    public int TownId { get; set; }

    [Required]
    [Column(TypeName = "NVARCHAR(100)")]
    public string Name { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Country))]
    public int CountryId { get; set; }

    public Country Country { get; set; } = null!;

    public ICollection<Team> Teams { get; set; } = new HashSet<Team>();
    public ICollection<Player> Players { get; set; } = new HashSet<Player>();
}