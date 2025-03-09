using System.ComponentModel.DataAnnotations;
using static Eventmi.Shared.DataConstraints.Event;

namespace Eventmi.Web.ViewModels.Event;

public class EventDisplayModel
{

	[Required]
	public int Id { get; set; }

	[Required]
	[MaxLength(NameMaxLength)]
	public string Name { get; set; } = null!;

	[Required]
	public DateTime End { get; set; }
}