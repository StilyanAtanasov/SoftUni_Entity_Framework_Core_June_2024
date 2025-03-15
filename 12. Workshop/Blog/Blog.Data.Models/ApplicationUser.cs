using System.ComponentModel.DataAnnotations;
using static Blog.Common.EntityConstraints.ApplicationUser;

namespace Blog.Data.Models;

public class ApplicationUser
{
	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(UserNameMaxLength)]
	public string UserName { get; set; } = null!;

	[Required]
	[MaxLength(EmailMaxLength)]
	public string Email { get; set; } = null!;

	[Required]
	[MaxLength(PasswordMaxLength)]
	public string Password { get; set; } = null!;
}