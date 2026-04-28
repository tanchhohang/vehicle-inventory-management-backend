using System.ComponentModel.DataAnnotations;

namespace vehicle_management_backend.Services.Dto.Request;

public class AuthenticateUserDto
{
    [Required]
    public string UsernameOrEmail { get; set; }

    [Required]
    public string Password { get; set; }
}