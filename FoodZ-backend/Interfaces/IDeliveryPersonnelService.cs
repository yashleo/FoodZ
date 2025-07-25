using Foodz.API.DTOs.DeliveryPersonnel;

namespace Foodz.API.Interfaces;

public interface IDeliveryPersonnelService
{
    Task<IEnumerable<DeliveryPersonnelReadDto>> GetAllAsync();
    Task<DeliveryPersonnelReadDto> GetByIdAsync(int id);
    Task<DeliveryPersonnelReadDto> CreateAsync(DeliveryPersonnelCreateDto dto);
    Task<bool> DeleteAsync(int id);

}