namespace Foodz.API.DTOs.MenuItem;


public class MenuItemCreateDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
}
