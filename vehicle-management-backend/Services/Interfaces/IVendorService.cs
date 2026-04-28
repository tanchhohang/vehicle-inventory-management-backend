// IVendorService.cs
// This is the interface for vendor service
// It works like a contract - it lists all the methods
// that VendorService.cs must implement
// I followed the same pattern as IAuthService.cs


using vehicle_management_backend.Models;
using vehicle_management_backend.Services.Dto.Request;

namespace vehicle_management_backend.Services.Interfaces;

public interface IVendorService
{
    // get all vendors from database
    Task<List<Vendor>> GetAllVendorsAsync();

    // get one vendor by their id
    Task<Vendor?> GetVendorByIdAsync(long id);

    // add a new vendor
    Task<string> AddVendorAsync(VendorDto vendorDto);

    // update an existing vendor
    Task<string> UpdateVendorAsync(long id, VendorDto vendorDto);

    // delete a vendor
    Task<string> DeleteVendorAsync(long id);
}