using System.ComponentModel.DataAnnotations;
using static Eventmi.Shared.DataConstraints.Event;

namespace Eventmi.Web.ViewModels.Event;

public class EventFormModel
{
    [Required]
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
    public string Town { get; set; } = null!;
}