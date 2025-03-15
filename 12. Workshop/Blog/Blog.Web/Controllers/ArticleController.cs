using Blog.Core.DTOs;
using Blog.Core.Entities.Enums;
using Blog.Core.Exceptions;
using Blog.Core.Interfaces.Services;
using Blog.Web.ViewModels.Article;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Controllers;

public class ArticleController : Controller
{
    private readonly IArticleService _articleService;

    public ArticleController(IArticleService articleService) => _articleService = articleService;

    [HttpGet]
    [Authorize]
    public IActionResult Add()
    {
        var genres = Enum.GetValues(typeof(Genre))
            .Cast<Genre>()
            .Select(g => new SelectListItem
            {
                Value = ((int)g).ToString(),
                Text = g.ToString()
            }).ToList();

        ViewBag.Genres = genres;

        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add(ArticleAddViewModel model)
    {
        if (!(User.Identity?.IsAuthenticated ?? false)) RedirectToAction("Index", "Home");
        if (!ModelState.IsValid) return View(model);

        try
        {
            await _articleService.AddArticleAsync(new AddArticleDto
            {
                Title = model.Title,
                Content = model.Content,
                Genre = (Genre)model.GenreId
            });
        }
        catch (FieldValidationException ex)
        {
            ModelState.AddModelError(ex.FieldName, ex.Message);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult All()
    {
        ArticleViewModel[] articleViewModels = _articleService
            .AllArticlesAsync()
            .Select(a => new ArticleViewModel
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                Genre = a.Genre.ToString(),
                CreatedOn = a.CreatedOn,
                Author = a.Author,
            })
            .ToArray();

        return View(articleViewModels);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return RedirectToAction("All");

        ArticleDto dto = await _articleService.GetArticle(id.Value);
        ArticleViewModel model = new ArticleViewModel
        {
            Id = dto.Id,
            Title = dto.Title,
            Content = dto.Content,
            Genre = dto.Genre,
            CreatedOn = dto.CreatedOn,
            Author = dto.Author
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return RedirectToAction("All");

        await _articleService.DeleteArticleAsync(id.Value);
        return RedirectToAction("All");
    }
}