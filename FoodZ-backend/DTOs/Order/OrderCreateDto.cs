using Foodz.API.DTOs.Order;

namespace Foodz.API.DTOs.Order;

public class OrderCreateDTO
{
    public string DeliveryAddress { get; set; }
    public List<OrderItemCreateDto> OrderItems { get; set; }
}
