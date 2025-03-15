using Blog.Core.DTOs;
using Blog.Core.Entities;
using Blog.Core.Interfaces.Repository;
using Blog.Core.Interfaces.Services;
using static Blog.Common.UiConstraints;

namespace Blog.Core.Services;

public class ArticleService : IArticleService
{
    private readonly IRepository _repository;

    public ArticleService(IRepository repository) => _repository = repository;

    public async Task<int> AddArticleAsync(AddArticleDto dto)
    {
        Article article = new()
        {
            Title = dto.Title,
            Content = dto.Content,
            Genre = dto.Genre,
            CreatedOn = DateTime.Now
        };

        await _repository.AddAsync(article);
        int rowUpdated = await _repository.SaveChangesAsync();

        return rowUpdated;
    }

    public ArticleCardDto[] AllArticlesAsync() =>
        _repository.AllReadonly<Article>()
            .Select(a => new ArticleCardDto
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content.Length < BlogCardContentMaxLength
                    ? a.Content
                    : $"{a.Content.Substring(0, BlogCardContentMaxLength)}...",
                Genre = a.Genre.ToString(),
                CreatedOn = a.CreatedOn,
                Author = a.Author.Username,
            })
            .ToArray();
}