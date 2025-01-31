using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(40)")]
    public string Username { get; set; } = null!;

    [Required]
    [Column(TypeName = "NVARCHAR(150)")]
    public string Name { get; set; } = null!;

    [Required]
    [Column(TypeName = "VARCHAR(100)")]
    public string Password { get; set; } = null!;

    [Required]
    [Column(TypeName = "VARCHAR(100)")]
    public string Email { get; set; } = null!;

    [Required]
    public decimal Balance { get; set; }

    public ICollection<Bet> Bets { get; set; } = new HashSet<Bet>();
}