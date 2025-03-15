using System.ComponentModel.DataAnnotations;
using static Blog.Common.EntityConstraints.ApplicationUser;

namespace Blog.Web.ViewModels.ApplicationUser;

public class LoginViewModel
{
    [Required]
    [MinLength(UserNameMinLength), MaxLength(UserNameMaxLength)]
    public string Username { get; set; } = null!;

    [Required]
    [MinLength(PasswordMinLength), MaxLength(PasswordMaxLength)]
    public string Password { get; set; } = null!;
}