using Microsoft.AspNetCore.Mvc;
using vehicle_management_backend.Services.Dto.Request;
using vehicle_management_backend.Services.Interfaces;

namespace vehicle_management_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PartsController : ControllerBase
{
    private readonly IPartsService _partsService;

    public PartsController(IPartsService partsService)
    {
        _partsService = partsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var parts = await _partsService.GetAllParts();
        return Ok(parts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var part = await _partsService.GetPartById(id);
        if (part == null) return NotFound();
        return Ok(part);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePartDto dto)
    {
        var part = await _partsService.CreatePart(dto);
        return CreatedAtAction(nameof(GetById), new { id = part.Id }, part);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, CreatePartDto dto)
    {
        var part = await _partsService.UpdatePart(id, dto);
        if (part == null) return NotFound();
        return Ok(part);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _partsService.DeletePart(id);
        if (!result) return NotFound();
        return NoContent();
    }
}