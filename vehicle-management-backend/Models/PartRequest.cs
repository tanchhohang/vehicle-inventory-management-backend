namespace vehicle_management_backend.Models;

public class PartRequest
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string PartName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime RequestedAt { get; set; }
}
