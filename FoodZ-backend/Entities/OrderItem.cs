namespace Foodz.API.Entitities;

public class OrderItem
{
    public int OrderID { get; set; } // Composite PK, FK
    public int MenuItemID { get; set; } // Composite PK, FK
    public int Quantity { get; set; }
    public decimal PriceAtOrderTime { get; set; }

    public Order Order { get; set; }
    public MenuItem MenuItem { get; set; }
}
