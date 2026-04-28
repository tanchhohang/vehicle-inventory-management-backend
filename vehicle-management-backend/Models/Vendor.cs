// Vendor.cs
// This is the Vendor model - it represents the vendors table in our database
// A vendor is a supplier who provides vehicle parts to our shop
// I followed the same pattern as Users.cs


using System.ComponentModel.DataAnnotations;

namespace vehicle_management_backend.Models;

public class Vendor
{
    // primary key - auto generated
    public long Id { get; set; }

    // vendor company name - required
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    // vendor email address - required
    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; }

    // vendor phone number - required
    [Required]
    [StringLength(15)]
    public string Phone { get; set; }

    // vendor address - required
    [Required]
    [StringLength(200)]
    public string Address { get; set; }

    // date when vendor was added to the system
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
