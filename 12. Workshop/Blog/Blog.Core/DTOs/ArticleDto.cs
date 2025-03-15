namespace Blog.Core.DTOs;

public class ArticleDto
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string Author { get; set; } = null!;
}