using System.ComponentModel.DataAnnotations;
using static Boardgames.Data.DataConstraints;

namespace Boardgames.Data.Models;

public class Seller
{
    public Seller() => BoardgamesSellers = new HashSet<BoardgameSeller>();

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(SellerNameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(SellerAddressMaxLength)]
    public string Address { get; set; } = null!;

    [Required]
    public string Country { get; set; } = null!;

    [Required]
    public string Website { get; set; } = null!;

    public ICollection<BoardgameSeller> BoardgamesSellers { get; set; }
}