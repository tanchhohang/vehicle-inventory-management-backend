using System.ComponentModel.DataAnnotations;

namespace vehicle_management_backend.Services.Dto.Request;

public class RegisterUserDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public string Role { get; set; } = "Customer";

    public string? VehicleNumber { get; set; }
}