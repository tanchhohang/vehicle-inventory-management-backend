using vehicle_management_backend.Models;
using vehicle_management_backend.Services.Dto.Request;

namespace vehicle_management_backend.Services.Interfaces;

public interface ISalesService
{
    Task<List<Sale>> GetAllSales();
    Task<Sale?> GetSaleById(long id);
    Task<Sale> CreateSale(CreateSaleDto dto);
}