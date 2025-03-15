using Blog.Core.DTOs;
namespace Blog.Core.Interfaces.Services;

public interface IArticleService
{
    Task<int> AddArticleAsync(AddArticleDto dto);

    Task<ArticleDto> GetArticle(int id);

    Task<int> DeleteArticleAsync(int id);

    ArticleDto[] AllArticlesAsync();
}