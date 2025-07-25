using AutoMapper;
using Foodz.API.Data;
using Foodz.API.DTOs.DeliveryPersonnel;
using Foodz.API.Entitities;
using Foodz.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Foodz.API.Services
{
    public class DeliveryPersonnelService : IDeliveryPersonnelService
    {
        private readonly FoodZDbContext _context;
        private readonly IMapper _mapper;

        public DeliveryPersonnelService(FoodZDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DeliveryPersonnelReadDto>> GetAllAsync()
        {
            var personnel = await _context.DeliveryPersonnels.ToListAsync();
            return _mapper.Map<IEnumerable<DeliveryPersonnelReadDto>>(personnel);
        }

        public async Task<DeliveryPersonnelReadDto> GetByIdAsync(int id)
        {
            var personnel = await _context.DeliveryPersonnels.FindAsync(id);
            if (personnel == null)
            {
                throw new Exception("Delivery personnel not found.");
            }
            return _mapper.Map<DeliveryPersonnelReadDto>(personnel);
        }

        public async Task<DeliveryPersonnelReadDto> CreateAsync(DeliveryPersonnelCreateDto dto)
        {
            var personnel = _mapper.Map<DeliveryPersonnel>(dto);

            _context.DeliveryPersonnels.Add(personnel);
            await _context.SaveChangesAsync();

            return _mapper.Map<DeliveryPersonnelReadDto>(personnel);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var personnel = await _context.DeliveryPersonnels.FindAsync(id);
            if (personnel == null)
            {
                return false;
            }

            _context.DeliveryPersonnels.Remove(personnel);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
