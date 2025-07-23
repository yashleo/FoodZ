namespace Foodz.API.Entitities;

public class MenuItem
{
    public int ID { get; set; } 
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }
}
