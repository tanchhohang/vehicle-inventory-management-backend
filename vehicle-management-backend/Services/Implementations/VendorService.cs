// VendorService.cs
// This is where I wrote the actual logic for all vendor operations
// It implements the IVendorService interface
// The controller will call these methods instead of talking to the database directly
// This keeps the controller clean - I learned this from our teacher

using Microsoft.EntityFrameworkCore;
using vehicle_management_backend.Data;
using vehicle_management_backend.Models;
using vehicle_management_backend.Services.Dto.Request;
using vehicle_management_backend.Services.Interfaces;

namespace vehicle_management_backend.Services.Implementations;

public class VendorService(AppDbContext db) : IVendorService
{
    // get all vendors from the database
    public async Task<List<Vendor>> GetAllVendorsAsync()
    {
        // fetch all vendors and order them by name
        return await db.Vendors.OrderBy(v => v.Name).ToListAsync();
    }

    // get a single vendor by id
    // returns null if vendor is not found
    public async Task<Vendor?> GetVendorByIdAsync(long id)
    {
        return await db.Vendors.FindAsync(id);
    }

    // add a new vendor to the database
    public async Task<string> AddVendorAsync(VendorDto dto)
    {
        // check if vendor with same email already exists
        var existingVendor = await db.Vendors
            .FirstOrDefaultAsync(v => v.Email == dto.Email);

        if (existingVendor != null)
            return "A vendor with this email already exists.";

        // create new vendor from dto data
        var vendor = new Vendor
        {
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            Address = dto.Address,
            CreatedAt = DateTime.UtcNow
        };

        // save to database
        db.Vendors.Add(vendor);
        await db.SaveChangesAsync();

        return "Vendor added successfully";
    }

    // update an existing vendor
    public async Task<string> UpdateVendorAsync(long id, VendorDto dto)
    {
        // find the vendor first
        var vendor = await db.Vendors.FindAsync(id);

        if (vendor == null)
            return "Vendor not found.";

        // check if another vendor already has this email
        var emailExists = await db.Vendors
            .FirstOrDefaultAsync(v => v.Email == dto.Email && v.Id != id);

        if (emailExists != null)
            return "Another vendor with this email already exists.";

        // update the vendor fields
        vendor.Name = dto.Name;
        vendor.Email = dto.Email;
        vendor.Phone = dto.Phone;
        vendor.Address = dto.Address;

        // save changes
        await db.SaveChangesAsync();

        return "Vendor updated successfully";
    }

    // delete a vendor from database
    public async Task<string> DeleteVendorAsync(long id)
    {
        // find the vendor first
        var vendor = await db.Vendors.FindAsync(id);

        if (vendor == null)
            return "Vendor not found.";

        // remove and save
        db.Vendors.Remove(vendor);
        await db.SaveChangesAsync();

        return "Vendor deleted successfully";
    }
}