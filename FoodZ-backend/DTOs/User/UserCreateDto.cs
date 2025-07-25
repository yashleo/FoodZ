namespace Foodz.API.DTOs.User;

public class UserCreateDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Address { get; set; } = "";
    public string ContactNumber { get; set; } = "";
    public string Email { get; set; } = "";
    public string Role { get; set; } = "";
}
