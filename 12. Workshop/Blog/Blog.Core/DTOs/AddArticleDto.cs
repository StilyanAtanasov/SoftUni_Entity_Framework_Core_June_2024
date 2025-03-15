using Blog.Core.Entities.Enums;

namespace Blog.Core.DTOs;

public class AddArticleDto
{
	public string Title { get; set; } = null!;

	public string Content { get; set; } = null!;

	public Genre Genre { get; set; }
}