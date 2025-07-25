using AutoMapper;
using Foodz.API.Data;
using Foodz.API.DTOs.MenuItem;
using Foodz.API.Entitities;
using Foodz.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Foodz.API.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly FoodZDbContext _context;
        private readonly IMapper _mapper;

        public MenuItemService(FoodZDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuItemReadDto>> GetMenuItemsAsync()
        {
            var menuItems = await _context.MenuItems.ToListAsync();
            return _mapper.Map<IEnumerable<MenuItemReadDto>>(menuItems);
        }

        public async Task<MenuItemReadDto> GetMenuItemByIdAsync(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                throw new Exception("Menu item not found.");
            }
            return _mapper.Map<MenuItemReadDto>(menuItem);
        }

        public async Task<MenuItemReadDto> CreateMenuItemAsync(MenuItemCreateDto dto)
        {
            var menuItem = _mapper.Map<MenuItem>(dto);
            menuItem.CreatedAt = DateTime.UtcNow;

            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();

            return _mapper.Map<MenuItemReadDto>(menuItem);
        }

        public async Task<MenuItemReadDto> UpdateMenuItemAsync(int id, MenuItemCreateDto dto)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                throw new Exception("Menu item not found.");
            }

            _mapper.Map(dto, menuItem); // updates properties from dto
            menuItem.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return _mapper.Map<MenuItemReadDto>(menuItem);
        }

        public async Task<bool> DeleteMenuItemAsync(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                return false;
            }

            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
