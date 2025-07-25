using Foodz.API.DTOs.Order;

namespace Foodz.API.DTOs.Order;

public class OrderCreateDto
{
    public string DeliveryAddress { get; set; } = "";
    public List<OrderItemCreateDto> OrderItems { get; set; } = [];
}
