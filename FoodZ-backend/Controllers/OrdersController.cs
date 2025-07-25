using Foodz.API.DTOs.Order;
using Foodz.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Foodz.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // JWT required for all
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // POST: api/Orders
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderCreateDto dto)
        {
            var userId = GetUserId();
            var createdOrder = await _orderService.PlaceOrderAsync(userId, dto);
            return CreatedAtAction(nameof(GetOrderById), new { orderId = createdOrder.Id }, createdOrder);
        }

        // GET: api/Orders/user
        [HttpGet("user")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetUserOrders()
        {
            var userId = GetUserId();
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }

        // GET: api/Orders
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // GET: api/Orders/5
        [HttpGet("{orderId}")]
        [Authorize] // Admin or user
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var userId = GetUserId();
            var isAdmin = User.IsInRole("Admin");

            var order = await _orderService.GetOrderByIdAsync(orderId, userId, isAdmin);
            return Ok(order);
        }

        // PUT: api/Orders/5/status
        [HttpPut("{orderId}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OrderStatusChange(int orderId, [FromBody] OrderStatusChangeDto dto)
        {

            var updatedOrder = await _orderService.OrderStatusChangeAsync(orderId, true, dto);

            return Ok(updatedOrder);
        }


        #region Helpers
        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }
        #endregion
    }
}
