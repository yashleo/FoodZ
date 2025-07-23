using Foodz.API.Entitities.Enums;

namespace Foodz.API.Entitities;

public class DeliveryPersonnel
{
    public int Id { get; set; } // PK
    public string Name { get; set; }
    public string VehicleId { get; set; }
    public string ContactNumber { get; set; }
    public bool AvailabilityStatus { get; set; }

    public Order Order { get; set; }
}
