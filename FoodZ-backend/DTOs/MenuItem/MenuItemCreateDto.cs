namespace Foodz.API.DTOs.MenuItem;


public class MenuItemCreateDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; } = 0;
    public string? ImageUrl { get; set; }
}
