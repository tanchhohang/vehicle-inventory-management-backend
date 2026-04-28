namespace vehicle_management_backend.Models;
using System.Text.Json.Serialization;

public class SaleItem
{
    public long Id { get; set; }
    public long SaleId { get; set; }
    [JsonIgnore]
    public Sale Sale { get; set; } = null!;
    public long PartId { get; set; }
    public Part Part { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }
}