using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Blog.Common.EntityConstraints.Article;

namespace Blog.Data.Models;

public class Article
{
	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(TitleMaxLength)]
	public string Title { get; set; } = null!;

	[Required]
	[MaxLength(ContentMaxLength)]
	public string Content { get; set; } = null!;

	[Required]
	public DateTime CreatedOn { get; set; }

	[Required]
	[ForeignKey(nameof(Author))]
	public int AuthorId { get; set; }

	public ApplicationUser Author { get; set; } = null!;
}