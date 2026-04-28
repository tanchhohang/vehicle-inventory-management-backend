// VendorController.cs
// This is the API controller for vendor management
// It handles HTTP requests and calls VendorService to do the actual work
// I kept the controller thin - it only receives requests and returns responses
// I followed the same pattern as AuthController.cs
// Author: Sneha Agrawal
// Date: April 2026

using Microsoft.AspNetCore.Mvc;
using vehicle_management_backend.Services.Dto.Request;
using vehicle_management_backend.Services.Interfaces;

namespace vehicle_management_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendorController(IVendorService vendorService) : ControllerBase
{
    // GET api/vendor - get all vendors
    [HttpGet]
    public async Task<IActionResult> GetAllVendors()
    {
        var vendors = await vendorService.GetAllVendorsAsync();
        return Ok(vendors);
    }

    // GET api/vendor/5 - get one vendor by id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVendorById(long id)
    {
        var vendor = await vendorService.GetVendorByIdAsync(id);

        // if vendor not found return 404
        if (vendor == null)
            return NotFound(new { message = "Vendor not found." });

        return Ok(vendor);
    }

    // POST api/vendor - add a new vendor
    [HttpPost]
    public async Task<IActionResult> AddVendor(VendorDto vendorDto)
    {
        // check if form data is valid
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await vendorService.AddVendorAsync(vendorDto);

        if (result != "Vendor added successfully")
            return BadRequest(new { message = result });

        return Ok(new { message = result });
    }

    // PUT api/vendor/5 - update an existing vendor
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVendor(long id, VendorDto vendorDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await vendorService.UpdateVendorAsync(id, vendorDto);

        if (result != "Vendor updated successfully")
            return BadRequest(new { message = result });

        return Ok(new { message = result });
    }

    // DELETE api/vendor/5 - delete a vendor
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVendor(long id)
    {
        var result = await vendorService.DeleteVendorAsync(id);

        if (result != "Vendor deleted successfully")
            return NotFound(new { message = result });

        return Ok(new { message = result });
    }
}