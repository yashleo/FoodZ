using Foodz.API.DTOs.Auth;
using Foodz.API.DTOs.User;

namespace Foodz.API.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto dto);
}