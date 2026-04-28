using Microsoft.AspNetCore.Mvc;
using vehicle_management_backend.Services.Dto.Request;
using vehicle_management_backend.Services.Interfaces;

namespace vehicle_management_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterUserDto registerUserDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await authService.RegisterUserAsync(registerUserDto);

        if (result != "User created successfully")
            return BadRequest(new { message = result });

        return Ok(new { message = result });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> AuthenticateUser(AuthenticateUserDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await authService.AuthenticateUserAsync(loginDto);

        if (result != "Login successful")
            return BadRequest(new { message = result });

        return Ok(new { message = result });
    }
}
