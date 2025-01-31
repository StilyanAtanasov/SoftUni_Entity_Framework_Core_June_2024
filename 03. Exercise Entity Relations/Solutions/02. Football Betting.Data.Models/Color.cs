using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models;

public class Color
{
    [Key]
    public int ColorId { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(15)")]
    public string Name { get; set; } = null!;

    [InverseProperty("PrimaryKitColor")]
    public ICollection<Team> PrimaryKitTeams { get; set; } = new List<Team>();

    [InverseProperty("SecondaryKitColor")]
    public ICollection<Team> SecondaryKitTeams { get; set; } = new List<Team>();
}