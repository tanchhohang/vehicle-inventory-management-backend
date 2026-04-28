using Microsoft.AspNetCore.Identity;
using vehicle_management_backend.Models;
using vehicle_management_backend.Services.Dto.Request;
using vehicle_management_backend.Services.Interfaces;

namespace vehicle_management_backend.Services.Implementations;

public class AuthService(UserManager<Users> userManager) : IAuthService
{
    public async Task<string> RegisterUserAsync(RegisterUserDto dto)
    {
        // Check if username already exists
        var existingUsername = await userManager.FindByNameAsync(dto.Username);
        if (existingUsername != null)
            return "Username is already taken.";

        // Check if email already exists
        var existingEmail = await userManager.FindByEmailAsync(dto.Email);
        if (existingEmail != null)
            return "An account with this email already exists.";

        var user = new Users
        {
            UserName = dto.Username,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Role = dto.Role,
            VehicleNumber = dto.VehicleNumber,
            Points = 0,
        };

        var result = await userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return result.Errors.Select(e => e.Description).First();

        return "User created successfully";
    }
    
    public async Task<string> AuthenticateUserAsync(AuthenticateUserDto dto)
    {
        // Find by username or email
        var user = await userManager.FindByNameAsync(dto.UsernameOrEmail)
                   ?? await userManager.FindByEmailAsync(dto.UsernameOrEmail);

        if (user == null)
            return "Invalid username or email.";

        var isPasswordValid = await userManager.CheckPasswordAsync(user, dto.Password);
        if (!isPasswordValid)
            return "Invalid password.";

        return "Login successful";
    }
}