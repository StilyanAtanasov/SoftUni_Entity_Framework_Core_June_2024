using Blog.Core.DTOs;
using Blog.Core.Entities.Enums;
using Blog.Core.Interfaces.Services;
using Blog.Web.ViewModels.Article;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Controllers;

public class ArticleController : Controller
{
    private readonly IArticleService _articleService;

    public ArticleController(IArticleService articleService) => _articleService = articleService;

    [HttpGet]
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
    public async Task<IActionResult> Add(ArticleAddViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        await _articleService.AddArticleAsync(new AddArticleDto
        {
            Title = model.Title,
            Content = model.Content,
            Genre = (Genre)model.GenreId
        });

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult All()
    {
        ArticleCardViewModel[] articleViewModels = _articleService
            .AllArticlesAsync()
            .Select(a => new ArticleCardViewModel
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
}