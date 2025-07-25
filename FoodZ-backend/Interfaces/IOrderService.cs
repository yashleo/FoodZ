using Foodz.API.DTOs.Order;

namespace Foodz.API.Interfaces;

public interface IOrderService
{
    Task<OrderReadDto> PlaceOrderAsync(int UserId, OrderCreateDto dto);
    Task<IEnumerable<OrderReadDto>> GetOrdersByUserIdAsync(int UserId);
    Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync();
    Task<OrderReadDto> GetOrderByIdAsync(int orderId, int userId, bool isAdmin);

}