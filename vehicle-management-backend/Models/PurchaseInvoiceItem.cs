namespace vehicle_management_backend.Models;

public class PurchaseInvoiceItem
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public int PartId { get; set; }
    public string PartName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
