
using Microsoft.EntityFrameworkCore;
using Foodz.API.Entitities;

namespace Foodz.API.Data;

public class FoodZDbContext : DbContext
{
    public FoodZDbContext(DbContextOptions<FoodZDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<DeliveryPersonnel> DeliveryPersonnels { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Composite PK for OrderItem
        modelBuilder.Entity<OrderItem>()
            .HasKey(oi => new { oi.OrderId, oi.MenuItemId });

        // User -> Orders (One to many)
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Order -> DeliveryPersonnel (one to one)
        modelBuilder.Entity<Order>()
            .HasOne(o => o.DeliveryPersonnel)
            .WithOne(dp => dp.Order)
            .HasForeignKey<Order>(o => o.DeliveryPersonnelId)
            .OnDelete(DeleteBehavior.Cascade);

        // OrderItem -> Order & MenuItem (many to many via orderitem)
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.MenuItem)
            .WithMany(mi => mi.OrderItems)
            .HasForeignKey(oi => oi.MenuItemId);
    }
}
