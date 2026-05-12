using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using vehicle_management_backend.Models;
using vehicle_management_backend.Services.Dto.Request;

namespace vehicle_management_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(UserManager<Users> userManager) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = userManager.Users.Select(u => new
        {
            u.Id,
            u.FirstName,
            u.LastName,
            u.UserName,
            u.Email,
            u.PhoneNumber,
            u.Role,
            u.VehicleNumber
        }).ToList();

        return Ok(users);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(long id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user == null) return NotFound();

        await userManager.DeleteAsync(user);
        return Ok(new { message = $"User {id} has been deleted" });
    }

    [HttpPatch("{id}/role")]
    public async Task<IActionResult> UpdateRole(long id, [FromBody] UpdateRoleDto dto)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user == null) return NotFound();

        var currentRoles = await userManager.GetRolesAsync(user);
        await userManager.RemoveFromRolesAsync(user, currentRoles);
        await userManager.AddToRoleAsync(user, dto.Role);
        
        user.Role = dto.Role;
        await userManager.UpdateAsync(user);

        return Ok();
    }
}