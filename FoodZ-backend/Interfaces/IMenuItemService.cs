using Foodz.API.DTOs.MenuItem;

namespace Foodz.API.Interfaces;

public interface IMenuItemService
{
    Task<IEnumerable<MenuItemReadDto>> GetMenuItemsAsync();
    Task<MenuItemReadDto> GetMenuItemByIdAsync(int id);
    Task<MenuItemReadDto> CreateMenuItemAsync(MenuItemCreateDto dto);
    Task<MenuItemReadDto> UpdateMenuItemAsync(int id, MenuItemCreateDto dto);
    Task<bool> DeleteMenuItemAsync(int id);
}