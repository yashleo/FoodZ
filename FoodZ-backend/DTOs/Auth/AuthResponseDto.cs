namespace Foodz.API.DTOs.Auth;


public class AuthResponseDTO
{
    public string Token { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string Name { get; set; }
}