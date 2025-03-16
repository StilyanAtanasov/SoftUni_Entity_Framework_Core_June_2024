using Blog.Core.DTOs;
namespace Blog.Core.Interfaces.Services;

public interface IArticleService
{
    Task<int> AddArticleAsync(AddArticleDto dto);

    Task<ArticleDto> GetArticle(int id);

    ArticleDto[] AllArticlesAsync();

    Task<ArticleDto> UpdateArticleAsync(UpdateArticleDto dto);

    Task<int> DeleteArticleAsync(int id);
}