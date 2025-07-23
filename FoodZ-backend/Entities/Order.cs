using Foodz.API.Entitities.Enums;

namespace Foodz.API.Entitities;

public class Order
{
    public int Id { get; set; } // PK
    public int UserId { get; set; } // FK
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string DeliveryAddress { get; set; } = "";
    public int DeliveryPersonID { get; set; }
    public OrderStatus Status { get; set; }


    public int DeliveryPersonnelId { get; set; } // FK

    public User User { get; set; }
    public DeliveryPersonnel DeliveryPersonnel { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}
