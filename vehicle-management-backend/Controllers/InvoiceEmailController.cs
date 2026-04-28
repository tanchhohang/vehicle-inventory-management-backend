using Microsoft.AspNetCore.Mvc;
using vehicle_management_backend.Services.Interfaces;

namespace vehicle_management_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoiceEmailController(IEmailService emailService)
    : ControllerBase
{
    [HttpPost("send/{saleId}")]
    public async Task<IActionResult> SendInvoiceEmail(long saleId)
    {
        try
        {
            await emailService.SendInvoiceEmailAsync(saleId);
            return Ok(new { message = "Invoice emailed successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}