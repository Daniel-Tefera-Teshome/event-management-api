using EventManagement.DTOs.Auth;

namespace EventManagement.Api.Services;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
    Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
    Task<AuthResponseDto> LogoutAsync(string refreshToken);
    Task LogoutAllAsync(Guid userId);

}