namespace Foodz.API.DTOs.Order;

public class OrderReadDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string DeliveryAddress { get; set; }
    public string Status { get; set; }
    public List<OrderItemReadDTO> OrderItems { get; set; }
}
