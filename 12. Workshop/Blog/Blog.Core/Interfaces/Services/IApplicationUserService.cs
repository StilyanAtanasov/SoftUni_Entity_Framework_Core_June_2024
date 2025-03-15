using Blog.Core.DTOs;

namespace Blog.Core.Interfaces.Services;

public interface IApplicationUserService
{
    Task AddUserAsync(ApplicationUserDto dto);

    Task LogInAsync(LogInUserDto dto);

    Task LogOutAsync();
}