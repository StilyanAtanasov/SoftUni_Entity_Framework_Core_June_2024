using Blog.Core.Entities.Enums;

namespace Blog.Core.DTOs;

public class UpdateArticleDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public Genre Genre { get; set; }
}