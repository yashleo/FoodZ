using Foodz.API.DTOs.User;

namespace Foodz.API.Interfaces;

public interface IUserService
{
    Task<UserReadDto> GetUserByIdAsync(int id);
    Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
    Task<UserReadDto> CreateUserAsync(UserCreateDto dto);
    Task<UserReadDto> UpdateUserAsync(int id, UserCreateDto dto);
    Task<bool> DeleteUserAsync(int id);
}