using Blog.Core.DTOs;
using Blog.Core.Entities;
using Blog.Core.Exceptions;
using Blog.Core.Interfaces.Repository;
using Blog.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using static Blog.Common.UiConstraints;

namespace Blog.Core.Services;

public class ArticleService : IArticleService
{
    private readonly IRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ArticleService(IRepository repository, IHttpContextAccessor accessor)
    {
        _repository = repository;
        _httpContextAccessor = accessor;
    }

    public async Task<int> AddArticleAsync(AddArticleDto dto)
    {
        bool success = Guid.TryParse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
            out Guid authorId);
        if (!success) throw new FieldValidationException("", "Error adding article");

        Article article = new()
        {
            Title = dto.Title,
            Content = dto.Content,
            Genre = dto.Genre,
            CreatedOn = DateTime.Now,
            AuthorId = authorId
        };

        await _repository.AddAsync(article);
        int rowUpdated = await _repository.SaveChangesAsync();

        return rowUpdated;
    }

    public async Task<ArticleDto> GetArticle(int id)
    {
        Article a = (await _repository.FindByExpressionAsync<Article>(a => a.Id == id,
            a => a.Author))!;
        return new ArticleDto
        {
            Id = a.Id,
            Title = a.Title,
            Content = a.Content,
            Genre = a.Genre.ToString(),
            CreatedOn = a.CreatedOn,
            Author = a.Author.Username
        };
    }

    public ArticleDto[] AllArticlesAsync() =>
        _repository.AllReadonly<Article>()
            .Select(a => new ArticleDto
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

    public async Task<int> DeleteArticleAsync(int id)
    {
        _repository.Delete((await _repository.GetByIdAsync<Article>(id))!);
        return await _repository.SaveChangesAsync();
    }
}