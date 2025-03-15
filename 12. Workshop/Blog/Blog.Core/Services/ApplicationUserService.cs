using Blog.Core.DTOs;
using Blog.Core.Entities;
using Blog.Core.Exceptions;
using Blog.Core.Interfaces.Repository;
using Blog.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Blog.Core.Services;

public class ApplicationUserService : IApplicationUserService
{
    private readonly IRepository _repository;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

    public ApplicationUserService(IRepository repository, IPasswordHasher<ApplicationUser> passwordHasher)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
    }

    public async Task AddUserAsync(ApplicationUserDto dto)
    {
        if (await IsUsernameOccupied(dto.Username)) throw new FieldValidationException("Username", "Username already taken!");
        if (await IsEmailOccupied(dto.Email)) throw new FieldValidationException("Email", "Email already registered!");

        ApplicationUser user = new()
        {
            Username = dto.Username,
            Email = dto.Email
        };

        user.Password = _passwordHasher.HashPassword(user, dto.Password);

        await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();
    }

    public Task<ApplicationUserDto?> GetUserByUsername(string username)
    {
        throw new NotImplementedException();
    }

    private async Task<bool> IsUsernameOccupied(string username)
        => await _repository.CheckExpressionAsync<ApplicationUser>(u => u.Username == username);

    private async Task<bool> IsEmailOccupied(string email)
        => await _repository.CheckExpressionAsync<ApplicationUser>(u => u.Email == email);
}