using Eventmi.Web.ViewModels.Event;
using System.ComponentModel.DataAnnotations;
using static Eventmi.Shared.DataConstraints.Town;

namespace Eventmi.Web.ViewModels.Town;

public class TownFormModel
{
	public TownFormModel() => Events = new HashSet<EventDisplayModel>();

	public int Id { get; set; }

	[Required]
	[MinLength(NameMinLength)]
	[MaxLength(NameMaxLength)]
	public string Name { get; set; } = null!;

	public ICollection<EventDisplayModel> Events { get; set; }
}