using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using static Blog.Common.EntityConstraints.Article;

namespace Blog.Web.ViewModels.Article;

public class AddArticleViewModel
{
    public AddArticleViewModel() => Genres = new();

    [Required]
    [MinLength(TitleMinLength)]
    [MaxLength(TitleMaxLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MinLength(ContentMinLength)]
    [MaxLength(ContentMaxLength)]
    public string Content { get; set; } = null!;

    [Required]
    public int GenreId { get; set; }

    public List<SelectListItem> Genres { get; set; }
}