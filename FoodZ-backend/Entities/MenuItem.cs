
namespace Foodz.API.Entitities;

public class MenuItem
{
    internal DateTime UpdatedAt;
    internal DateTime CreatedAt;

    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = "";

    public ICollection<OrderItem>? OrderItems { get; set; }
}
