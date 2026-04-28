using vehicle_management_backend.Services.Dto.Request;

namespace vehicle_management_backend.Services.Interfaces;

public interface IAuthService
{
    public Task<string> RegisterUserAsync(RegisterUserDto registerUserDto);
    Task<string> AuthenticateUserAsync(AuthenticateUserDto autheticateUserDto);
}