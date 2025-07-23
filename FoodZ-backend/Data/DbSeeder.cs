using Foodz.API.Data;
using Foodz.API.Entitities;
using Foodz.API.Entitities.Enums;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FoodOrderingSystem.API.Data
{
    public class DbSeeder
    {
        public static async Task SeedAsync(FoodZDbContext context)
        {
            if (!await context.Users.AnyAsync())
            {
                CreatePasswordHash("Admin@123", out byte[] adminHash, out byte[] adminSalt);
                CreatePasswordHash("User@123", out byte[] userHash, out byte[] userSalt);

                var admin = new User
                {
                    Name = "Admin User",
                    Email = "admin@foodz.com",
                    Address = "Admin Street",
                    ContactNumber = "9999999999",
                    PasswordHash = adminHash,
                    PasswordSalt = adminSalt,
                    Role = UserRole.Admin
                };

                var user = new User
                {
                    Name = "Regular User",
                    Email = "user@foodz.com",
                    Address = "User Street",
                    ContactNumber = "8888888888",
                    PasswordHash = userHash,
                    PasswordSalt = userSalt,
                    Role = UserRole.Customer
                };

                await context.Users.AddRangeAsync(admin, user);
                await context.SaveChangesAsync();
            }

            if (!await context.MenuItems.AnyAsync())
            {
                var items = new List<MenuItem>
                {
                    new MenuItem { Name = "Margherita Pizza", Description = "Classic cheese pizza", Price = 8.99m },
                    new MenuItem { Name = "Veg Burger", Description = "Crispy vegetable patty with sauces", Price = 5.49m },
                    new MenuItem { Name = "Pasta Alfredo", Description = "Creamy Alfredo sauce pasta", Price = 7.25m }
                };
                await context.MenuItems.AddRangeAsync(items);
                await context.SaveChangesAsync();
            }

            if (!await context.DeliveryPersonnels.AnyAsync())
            {
                var deliveryPersonnel = new DeliveryPersonnel
                {
                    Name = "Delivery Guy",
                    ContactNumber = "7777777777",
                    VehicleId = "WB20A1234",
                    AvailabilityStatus = true
                };
                await context.DeliveryPersonnels.AddAsync(deliveryPersonnel);
                await context.SaveChangesAsync();
            }
        }

        private static void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}