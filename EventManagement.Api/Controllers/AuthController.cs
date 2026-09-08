using System.Security.Claims;
using EventManagement.Api.Services;
using EventManagement.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(
        RegisterDto dto)
    {

        try
        {
            var result = await _authService.RegisterAsync(dto);

            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new
            {
                message = ex.Message
            });
        }
    }
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(
    LoginDto dto)
    {
        try
        {
            var result = await _authService.LoginAsync(dto);

            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new
            {
                message = ex.Message
            });
        }
    }
    [HttpPost("refresh")]
    public async Task<ActionResult<AuthResponseDto>> Refresh(
        RefreshTokenDto dto)
    {
        try
        {
            var result =
                await _authService.RefreshTokenAsync(
                    dto.RefreshToken);

            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new
            {
                message = ex.Message
            });
        }
    }
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(
     RefreshTokenDto dto)
    {
        await _authService.LogoutAsync(
            dto.RefreshToken);

        return Ok(new
        {
            message = "Logged out successfully."
        });
    }
    [Authorize]
    [HttpPost("logout-all")]
    public async Task<IActionResult> LogoutAll()
    {
        var userId = Guid.Parse(
            User.FindFirstValue(
                ClaimTypes.NameIdentifier)!);

        await _authService.LogoutAllAsync(userId);

        return Ok(new
        {
            message = "All sessions have been logged out."
        });
    }

}