using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models;

public class Team
{
    [Key]
    public int TeamId { get; set; }

    [Required]
    [Column(TypeName = "NVARCHAR(80)")]
    public string Name { get; set; } = null!;

    [Required]
    [Column(TypeName = "VARCHAR(150)")]
    public string LogoUrl { get; set; } = null!;

    [Required]
    [Column(TypeName = "NVARCHAR(10)")]
    public string Initials { get; set; } = null!;

    [Required]
    public decimal Budget { get; set; }

    [Required]
    [ForeignKey(nameof(PrimaryKitColor))]
    public int PrimaryKitColorId { get; set; }

    public Color PrimaryKitColor { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(SecondaryKitColor))]
    public int SecondaryKitColorId { get; set; }

    public Color SecondaryKitColor { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Town))]
    public int TownId { get; set; }

    public Town Town { get; set; } = null!;

    [InverseProperty("HomeTeam")]
    public ICollection<Game> HomeGames { get; set; } = new HashSet<Game>();

    [InverseProperty("AwayTeam")]
    public ICollection<Game> AwayGames { get; set; } = new HashSet<Game>();

    public ICollection<Player> Players { get; set; } = new HashSet<Player>();
}