namespace Foodz.API.DTOs.Order;

public class OrderItemReadDTO
{
    public int MenuItemId { get; set; }
    public string MenuItemName { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAtOrderTime { get; set; }
}