namespace vehicle_management_backend.Models;

public class Sale
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public Users Customer { get; set; } = null!;
    public long StaffId { get; set; }
    public DateTime SaleDate { get; set; } = DateTime.UtcNow;
    public decimal SubTotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public bool LoyaltyDiscountApplied { get; set; }
    public bool IsPaid { get; set; } = true;
    public List<SaleItem> SaleItems { get; set; } = new();
}