using Foodz.API.DTOs.Order;
using Foodz.API.Entitities.Enums;

namespace Foodz.API.DTOs.Order;

public class OrderCreateDto
{
    public string DeliveryAddress { get; set; } = "";
    public List<OrderItemCreateDto> OrderItems { get; set; } = [];
}

public class OrderStatusChangeDto
{
    public string? Status { get; set; }
}