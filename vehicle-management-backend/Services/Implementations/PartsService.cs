using Microsoft.EntityFrameworkCore;
using vehicle_management_backend.Data;
using vehicle_management_backend.Models;
using vehicle_management_backend.Services.Dto.Request;
using vehicle_management_backend.Services.Interfaces;

namespace vehicle_management_backend.Services.Implementations;

public class PartsService : IPartsService
{
    private readonly AppDbContext _context;

    public PartsService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Part>> GetAllParts()
    {
        return await _context.Parts.ToListAsync();
    }

    public async Task<Part?> GetPartById(long id)
    {
        return await _context.Parts.FindAsync(id);
    }

    public async Task<Part> CreatePart(CreatePartDto dto)
    {
        var part = new Part
        {
            Name = dto.Name,
            Description = dto.Description,
            Category = dto.Category,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Parts.Add(part);
        await _context.SaveChangesAsync();
        return part;
    }

    public async Task<Part?> UpdatePart(long id, CreatePartDto dto)
    {
        var part = await _context.Parts.FindAsync(id);
        if (part == null) return null;

        part.Name = dto.Name;
        part.Description = dto.Description;
        part.Category = dto.Category;
        part.Price = dto.Price;
        part.StockQuantity = dto.StockQuantity;
        part.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return part;
    }

    public async Task<bool> DeletePart(long id)
    {
        var part = await _context.Parts.FindAsync(id);
        if (part == null) return false;

        _context.Parts.Remove(part);
        await _context.SaveChangesAsync();
        return true;
    }
}