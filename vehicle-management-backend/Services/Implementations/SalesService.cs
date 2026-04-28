using Microsoft.EntityFrameworkCore;
using vehicle_management_backend.Data;
using vehicle_management_backend.Models;
using vehicle_management_backend.Services.Dto.Request;
using vehicle_management_backend.Services.Interfaces;

namespace vehicle_management_backend.Services.Implementations;

public class SalesService : ISalesService
{
    private readonly AppDbContext _context;

    public SalesService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Sale>> GetAllSales()
    {
        return await _context.Sales
            .Include(s => s.SaleItems)
            .ThenInclude(si => si.Part)
            .ToListAsync();
    }

    public async Task<Sale?> GetSaleById(long id)
    {
        return await _context.Sales
            .Include(s => s.SaleItems)
            .ThenInclude(si => si.Part)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Sale> CreateSale(CreateSaleDto dto)
    {
        var saleItems = new List<SaleItem>();
        decimal subTotal = 0;

        foreach (var item in dto.Items)
        {
            var part = await _context.Parts.FindAsync(item.PartId);
            if (part == null)
                throw new Exception($"Part with ID {item.PartId} not found.");

            if (part.StockQuantity < item.Quantity)
                throw new Exception($"Insufficient stock for part '{part.Name}'. Available: {part.StockQuantity}");

            var lineTotal = part.Price * item.Quantity;
            subTotal += lineTotal;

            saleItems.Add(new SaleItem
            {
                PartId = part.Id,
                Quantity = item.Quantity,
                UnitPrice = part.Price,
                LineTotal = lineTotal
            });

            part.StockQuantity -= item.Quantity;
        }

        bool loyaltyApplied = subTotal > 5000;
        decimal discountAmount = loyaltyApplied ? subTotal * 0.10m : 0;
        decimal totalAmount = subTotal - discountAmount;

        var sale = new Sale
        {
            CustomerId = dto.CustomerId,
            StaffId = dto.StaffId,
            SaleDate = DateTime.UtcNow,
            SubTotal = subTotal,
            DiscountAmount = discountAmount,
            TotalAmount = totalAmount,
            LoyaltyDiscountApplied = loyaltyApplied,
            SaleItems = saleItems
        };

        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();
        return sale;
    }
}