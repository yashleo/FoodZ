namespace Foodz.API.DTOs.DeliveryPersonnel;

public class DeliveryPersonnelReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string VehicleId { get; set; } = "";
    public string ContactNumber { get; set; } = "";
    public bool AvailabilityStatus { get; set; }
}
