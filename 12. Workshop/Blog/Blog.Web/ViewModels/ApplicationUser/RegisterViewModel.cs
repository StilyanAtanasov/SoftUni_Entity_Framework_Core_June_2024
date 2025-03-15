using System.ComponentModel.DataAnnotations;
using static Blog.Common.EntityConstraints.ApplicationUser;

namespace Blog.Web.ViewModels.ApplicationUser;

public class RegisterViewModel
{
    [Required]
    [MinLength(UserNameMinLength), MaxLength(UserNameMaxLength)]
    public string Username { get; set; } = null!;

    [Required, EmailAddress]
    [MinLength(EmailMinLength), MaxLength(EmailMaxLength)]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(PasswordMinLength), MaxLength(PasswordMaxLength)]
    public string Password { get; set; } = null!;

    [Required, Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = null!;
}