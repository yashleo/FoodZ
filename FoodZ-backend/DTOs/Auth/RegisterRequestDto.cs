using Foodz.API.Entitities.Enums;

namespace Foodz.API.DTOs.Auth;

public class RegisterRequestDto
{
    public string Name { get; set; } = "";
    public string Address { get; set; } = "";
    public string ContactNumber { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string Role { get; set; } = "User"; // "User" or "Admin"
     public string? Passkey { get; set; } // Required if selected Role == "Admin"
}
