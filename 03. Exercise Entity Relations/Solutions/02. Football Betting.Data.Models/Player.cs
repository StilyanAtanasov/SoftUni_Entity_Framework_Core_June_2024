using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models;

public class Player
{
    [Key]
    public int PlayerId { get; set; }

    [Required]
    [Column(TypeName = "NVARCHAR(150)")]
    public string Name { get; set; } = null!;

    [Required]
    public int SquadNumber { get; set; }

    [Required]
    public bool IsInjured { get; set; }

    [Required]
    [ForeignKey(nameof(Position))]
    public int PositionId { get; set; }

    public Position Position { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Team))]
    public int TeamId { get; set; }

    public Team Team { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Town))]
    public int TownId { get; set; }

    public Town Town { get; set; } = null!;

    public ICollection<PlayerStatistic> PlayersStatistics { get; set; } = new HashSet<PlayerStatistic>();
}