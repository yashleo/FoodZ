using AutoMapper;
using Foodz.API.Data;
using Foodz.API.DTOs.User;
using Foodz.API.Entitities;
using Foodz.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Foodz.API.Services
{
    public class UserService : IUserService
    {
        private readonly FoodZDbContext _context;
        private readonly IMapper _mapper;

        public UserService(FoodZDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserReadDto>>(users);
        }

        public async Task<UserReadDto> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<UserReadDto> CreateUserAsync(UserCreateDto dto)
        {
            var user = _mapper.Map<User>(dto);
            user.CreatedAt = DateTime.UtcNow;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<UserReadDto> UpdateUserAsync(int id, UserCreateDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            _mapper.Map(dto, user);
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
