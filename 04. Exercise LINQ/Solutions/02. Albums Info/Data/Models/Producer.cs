using System.ComponentModel.DataAnnotations;

namespace MusicHub.Data.Models;

public class Producer
{
    public Producer() => Albums = new HashSet<Album>();

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Name { get; set; } = null!;

    public string? Pseudonym { get; set; } = null!;

    public string? PhoneNumber { get; set; } = null!;

    public ICollection<Album> Albums { get; set; }
}