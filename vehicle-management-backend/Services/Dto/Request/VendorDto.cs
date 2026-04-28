// VendorDto.cs
// DTO = Data Transfer Object
// I use this instead of sending the full Vendor model to the API
// It only contains the fields the user needs to fill in
// I learned this pattern from our teacher - keep DTOs simple!


using System.ComponentModel.DataAnnotations;

namespace vehicle_management_backend.Services.Dto.Request;

// this DTO is used when adding or updating a vendor
public class VendorDto
{
    // vendor name is required
    [Required(ErrorMessage = "Vendor name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; }

    // email must be in correct format
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(100)]
    public string Email { get; set; }

    // phone must be provided
    [Required(ErrorMessage = "Phone number is required")]
    [StringLength(15, ErrorMessage = "Phone cannot exceed 15 characters")]
    public string Phone { get; set; }

    // address must be provided
    [Required(ErrorMessage = "Address is required")]
    [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
    public string Address { get; set; }
}