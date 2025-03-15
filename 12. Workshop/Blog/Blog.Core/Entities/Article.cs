using Blog.Core.Entities.Enums;

namespace Blog.Core.Entities;

public class Article
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public Genre Genre { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid AuthorId { get; set; }

    public ApplicationUser Author { get; set; } = null!;
}