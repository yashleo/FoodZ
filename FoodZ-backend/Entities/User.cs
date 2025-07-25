using Foodz.API.Entitities.Enums;
using Microsoft.AspNetCore.Identity;

namespace Foodz.API.Entitities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
    public UserRole Role { get; set; } = UserRole.User;
    // Consider using an enum
    public DateTime CreatedAt { get; set; }
    public ICollection<Order> Orders { get; set; } = [];
    public DateTime UpdatedAt { get; internal set; }
}
