using AutoMapper;
using Foodz.API.Data;
using Foodz.API.DTOs.Auth;
using Foodz.API.Entitities;
using Foodz.API.Entitities.Enums;
using Foodz.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Foodz.API.Profiles
{
    public class AuthService : IAuthService
    {
        private readonly FoodZDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthService(
            FoodZDbContext context,
            IMapper mapper,
            IConfiguration configuration,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            {
                throw new Exception("Email already registered.");
            }

            var user = _mapper.Map<User>(dto);

            // Handle role + passkey logic
            if (dto.Role == "Admin")
            {
                var expectedPasskey = _configuration["AdminCreation:Passkey"];
                if (dto.Passkey != expectedPasskey)
                {
                    throw new Exception("Invalid admin passkey.");
                }
                user.Role = UserRole.Admin;
            }
            else
            {
                user.Role = UserRole.User;
            }

            CreatePasswordHash(dto.Password, out byte[] hash, out byte[] salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Generate JWT
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponseDto
            {
                Email = user.Email,
                Name = user.Name,
                Role = user.Role.ToString(),
                Token = token
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
            {
                throw new Exception("Invalid credentials.");
            }

            if (!VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Invalid credentials.");
            }



            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponseDto
            {
                Email = user.Email,
                Name = user.Name,
                Role = user.Role.ToString(),
                Token = token
            };
        }

        private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(storedHash);
        }
    }
}
