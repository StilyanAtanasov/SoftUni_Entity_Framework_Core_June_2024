using Blog.Core.DTOs;
using Blog.Core.Entities;
using Blog.Core.Exceptions;
using Blog.Core.Interfaces.Repository;
using Blog.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Blog.Core.Services;

public class ApplicationUserService : IApplicationUserService
{
    private readonly IRepository _repository;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApplicationUserService(IRepository repository,
        IPasswordHasher<ApplicationUser> passwordHasher,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task AddUserAsync(ApplicationUserDto dto)
    {
        if (await IsUsernameOccupied(dto.Username)) throw new FieldValidationException(nameof(ApplicationUser.Username), "Username already taken!");
        if (await IsEmailOccupied(dto.Email)) throw new FieldValidationException(nameof(ApplicationUser.Email), "Email already registered!");

        ApplicationUser user = new()
        {
            Username = dto.Username,
            Email = dto.Email
        };

        user.Password = _passwordHasher.HashPassword(user, dto.Password);

        await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();
    }

    public async Task LogInAsync(LogInUserDto dto)
    {
        if (!await IsUsernameOccupied(dto.Username)) throw new FieldValidationException("", "Invalid credentials!");
        ApplicationUser user = (await GetUserByUsername(dto.Username))!;

        PasswordVerificationResult passwordValidity = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
        if (passwordValidity == PasswordVerificationResult.Failed) throw new FieldValidationException("", "Invalid credentials!");

        Claim[] claims =
        {
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Name, user.Username),
            new (ClaimTypes.Email, user.Email),
        };

        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await _httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));
    }

    public async Task LogOutAsync()
        => await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

    private async Task<ApplicationUser?> GetUserByUsername(string username)
        => await _repository.FindByExpressionAsync<ApplicationUser>(u => u.Username == username);


    private async Task<bool> IsUsernameOccupied(string username)
        => await _repository.CheckExpressionAsync<ApplicationUser>(u => u.Username == username);

    private async Task<bool> IsEmailOccupied(string email)
        => await _repository.CheckExpressionAsync<ApplicationUser>(u => u.Email == email);
}