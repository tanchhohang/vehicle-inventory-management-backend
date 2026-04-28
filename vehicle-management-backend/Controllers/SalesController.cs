using Microsoft.AspNetCore.Mvc;
using vehicle_management_backend.Services.Dto.Request;
using vehicle_management_backend.Services.Interfaces;

namespace vehicle_management_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ISalesService _salesService;

    public SalesController(ISalesService salesService)
    {
        _salesService = salesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sales = await _salesService.GetAllSales();
        return Ok(sales);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var sale = await _salesService.GetSaleById(id);
        if (sale == null) return NotFound();
        return Ok(sale);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSaleDto dto)
    {
        try
        {
            var sale = await _salesService.CreateSale(dto);
            return CreatedAtAction(nameof(GetById), new { id = sale.Id }, sale);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}