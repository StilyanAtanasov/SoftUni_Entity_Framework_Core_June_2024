using Blog.Core.DTOs;
using Blog.Core.Exceptions;
using Blog.Core.Interfaces.Services;
using Blog.Web.ViewModels.ApplicationUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers;

public class ApplicationUserController : Controller
{
    private readonly IApplicationUserService _applicationUserService;

    public ApplicationUserController(IApplicationUserService service) => _applicationUserService = service;

    [HttpGet]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated ?? false) RedirectToAction("All", "Article");

        var model = new RegisterViewModel();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        try
        {
            await _applicationUserService.AddUserAsync(new ApplicationUserDto
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password
            });
        }
        catch (FieldValidationException e)
        {
            ModelState.AddModelError(e.FieldName, e.Message);
            return View(model);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated ?? false) RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        try
        {
            await _applicationUserService.LogInAsync(new LogInUserDto
            {
                Username = model.Username,
                Password = model.Password
            });
        }
        catch (FieldValidationException e)
        {
            ModelState.AddModelError(e.FieldName, e.Message);
            return View(model);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        if (!(User.Identity?.IsAuthenticated ?? false)) RedirectToAction("Index", "Home");

        await _applicationUserService.LogOutAsync();
        return RedirectToAction("Index", "Home");
    }
}