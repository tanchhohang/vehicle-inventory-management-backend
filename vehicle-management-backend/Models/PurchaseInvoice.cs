namespace vehicle_management_backend.Models;

public class PurchaseInvoice
{
    public int Id { get; set; }
    public string VendorName { get; set; } = string.Empty;
    public string PartName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
