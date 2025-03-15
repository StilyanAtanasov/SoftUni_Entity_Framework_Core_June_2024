using Blog.Core.DTOs;
using Blog.Core.Entities;
using Blog.Core.Exceptions;
using Blog.Core.Interfaces.Services;
using Blog.Web.ViewModels.ApplicationUser;
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

    /* [HttpGet]
     [AllowAnonymous]
     public IActionResult Login()
     {
         if (User?.Identity?.IsAuthenticated ?? false)
         {
             return RedirectToAction("All", "Article");
         }

         var model = new LoginViewModel();

         return View(model);
     }

     [HttpPost]
     [AllowAnonymous]
     public async Task<IActionResult> Login(LoginViewModel model)
     {
         if (!ModelState.IsValid)
         {
             return View(model);
         }

         var user = await userManager.FindByNameAsync(model.UserName);

         if (user != null)
         {
             var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

             if (result.Succeeded)
             {
                 return RedirectToAction("All", "Article");
             }
         }

         ModelState.AddModelError("", "Invalid login");

         return View(model);
     }

     public async Task<IActionResult> Logout()
     {
         await signInManager.SignOutAsync();

         return RedirectToAction("Index", "Home");
     }*/
}