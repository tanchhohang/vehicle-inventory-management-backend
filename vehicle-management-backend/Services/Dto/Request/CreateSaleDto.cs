namespace vehicle_management_backend.Services.Dto.Request;

public class CreateSaleDto
{
    public long CustomerId { get; set; }
    public long StaffId { get; set; }
    public List<SaleItemDto> Items { get; set; } = new();
}

public class SaleItemDto
{
    public long PartId { get; set; }
    public int Quantity { get; set; }
}