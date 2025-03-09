using System.ComponentModel.DataAnnotations;
using static Eventmi.Shared.DataConstraints.Town;

namespace Eventmi.Data.Models;

public class Town
{
    public Town() => Events = new HashSet<Event>();

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    public ICollection<Event> Events { get; set; }
}