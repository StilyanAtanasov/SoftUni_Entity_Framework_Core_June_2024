using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Eventmi.Shared.DataConstraints.Event;

namespace Eventmi.Data.Models;

public class Event
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    public DateTime Start { get; set; }

    [Required]
    public DateTime End { get; set; }

    [Required]
    public string Place { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Town))]
    public int TownId { get; set; }

    public Town Town { get; set; } = null!;
}