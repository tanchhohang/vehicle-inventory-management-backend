using Microsoft.AspNetCore.Mvc;
using vehicle_management_backend.Models;

namespace vehicle_management_backend.Controllers;

[ApiController]
[Route("api/purchase-invoices")]
public class PurchaseInvoiceController : ControllerBase
{
    private static readonly List<PurchaseInvoice> PurchaseInvoices = new();

    [HttpGet]
    public ActionResult<IEnumerable<PurchaseInvoice>> GetAll()
    {
        return Ok(PurchaseInvoices);
    }

    [HttpGet("{id:int}")]
    public ActionResult<PurchaseInvoice> GetById(int id)
    {
        var invoice = PurchaseInvoices.FirstOrDefault(i => i.Id == id);
        if (invoice is null)
        {
            return NotFound();
        }

        return Ok(invoice);
    }

    [HttpPost]
    public ActionResult<PurchaseInvoice> Create([FromBody] PurchaseInvoice invoice)
    {
        invoice.Id = PurchaseInvoices.Count == 0 ? 1 : PurchaseInvoices.Max(i => i.Id) + 1;

        PurchaseInvoices.Add(invoice);

        return CreatedAtAction(nameof(GetById), new { id = invoice.Id }, invoice);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var invoice = PurchaseInvoices.FirstOrDefault(i => i.Id == id);
        if (invoice is null)
        {
            return NotFound();
        }

        PurchaseInvoices.Remove(invoice);
        return NoContent();
    }
}
