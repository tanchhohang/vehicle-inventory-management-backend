using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace vehicle_management_backend.Models;

public class Users : IdentityUser<long>
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    public string Role { get; set; } = "Customer";

    public string? VehicleNumber { get; set; }

    public int Points { get; set; } = 0;
}