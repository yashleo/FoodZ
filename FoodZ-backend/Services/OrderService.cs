using AutoMapper;
using Foodz.API.Data;
using Foodz.API.DTOs.Order;
using Foodz.API.Entitities;
using Foodz.API.Entitities.Enums;
using Foodz.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Foodz.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly FoodZDbContext _context;
        private readonly IMapper _mapper;

        public OrderService(FoodZDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderReadDto> PlaceOrderAsync(int userId, OrderCreateDto dto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new Exception("User not found!");
            var order = new Order
            {
                User = user,
                OrderDate = DateTime.UtcNow,
                DeliveryAddress = user.Address,
                Status = OrderStatus.Pending,
            };
            /* var order = _mapper.Map<Order>(dto);
            order.UserId = userId;
            order.OrderDate = DateTime.UtcNow;
            order.Status = OrderStatus.Pending;
 */
            // Calculate total and add OrderItems
            decimal total = 0;
            order.OrderItems = new List<OrderItem>();

            foreach (var itemDto in dto.OrderItems)
            {
                var menuItem = await _context.MenuItems.FindAsync(itemDto.MenuItemId);
                if (menuItem == null)
                {
                    throw new Exception($"Menu item with ID {itemDto.MenuItemId} not found.");
                }

                var orderItem = new OrderItem
                {
                    MenuItemId = itemDto.MenuItemId,
                    Quantity = itemDto.Quantity,
                    PriceAtOrderTime = menuItem.Price
                };

                total += menuItem.Price * itemDto.Quantity;
                order.OrderItems.Add(orderItem);
            }

            order.TotalAmount = total;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderReadDto>(order);
        }

        public async Task<IEnumerable<OrderReadDto>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.UserId == userId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderReadDto>>(orders);
        }

        public async Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .Include(o => o.User)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderReadDto>>(orders);
        }

        public async Task<OrderReadDto> GetOrderByIdAsync(int orderId, int userId, bool isAdmin)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            if (!isAdmin && order.UserId != userId)
            {
                throw new Exception("Access denied.");
            }

            return _mapper.Map<OrderReadDto>(order);
        }

        public async Task<OrderReadDto> OrderStatusChangeAsync(int orderId, bool isAdmin, OrderStatusChangeDto dto)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                throw new Exception("Order not found.");

            if (!isAdmin)
                throw new Exception("Access denied. Admins only.");

            if (!Enum.TryParse<OrderStatus>(dto.Status, true, out var statusEnum))
                throw new Exception($"Invalid status: {dto.Status}");

            order.Status = statusEnum;

            await _context.SaveChangesAsync();

            return _mapper.Map<OrderReadDto>(order);
        }


    }
}
