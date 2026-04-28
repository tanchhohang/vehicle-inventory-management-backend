namespace vehicle_management_backend.Models;

public class PurchaseInvoice
{
    public int Id { get; set; }
    public int VendorId { get; set; }
    public string VendorName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }
    public List<PurchaseInvoiceItem> Items { get; set; } = new();
}
