using Foodz.API.DTOs.MenuItem;
using Foodz.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FoodZ_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Require JWT for all endpoints
    public class MenuItemsController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemsController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        // GET: api/MenuItems
        [HttpGet]
        [AllowAnonymous] // Allow users to browse the menu without login
        public async Task<IActionResult> GetMenuItems()
        {
            var items = await _menuItemService.GetMenuItemsAsync();
            return Ok(items);
        }

        // GET: api/MenuItems/5
        [HttpGet("{id}")]
        [AllowAnonymous] // Allow users to view item details without login
        public async Task<IActionResult> GetMenuItemById(int id)
        {
            var item = await _menuItemService.GetMenuItemByIdAsync(id);
            return Ok(item);
        }

        // POST: api/MenuItems
        [HttpPost]
        [Authorize(Roles = "Admin")] // Only admin can create menu items
        public async Task<IActionResult> CreateMenuItem([FromBody] MenuItemCreateDto dto)
        {
            var createdItem = await _menuItemService.CreateMenuItemAsync(dto);
            return CreatedAtAction(nameof(GetMenuItemById), new { id = createdItem.Id }, createdItem);
        }

        // PUT: api/MenuItems/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMenuItem(int id, [FromBody] MenuItemCreateDto dto)
        {
            var existingItem = await _menuItemService.GetMenuItemByIdAsync(id);
            if (existingItem == null)
                return NotFound();

            // Apply only the fields that are provided
            if (!string.IsNullOrWhiteSpace(dto.Name))
                existingItem.Name = dto.Name;

            if (string.IsNullOrWhiteSpace(dto.Price.ToString()) && dto.Price > 0)
                existingItem.Price = dto.Price;

            if (!string.IsNullOrWhiteSpace(dto.Description))
                existingItem.Description = dto.Description;

            var updatedItem = existingItem;
            return Ok(updatedItem);
        }


        // DELETE: api/MenuItems/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only admin can delete menu items
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var deleted = await _menuItemService.DeleteMenuItemAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
