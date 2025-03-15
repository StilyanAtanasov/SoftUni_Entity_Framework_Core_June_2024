using Blog.Core.DTOs;

namespace Blog.Core.Interfaces.Services;

public interface IArticleService
{
    Task<int> AddArticleAsync(AddArticleDto dto);

    ArticleCardDto[] AllArticlesAsync();
}