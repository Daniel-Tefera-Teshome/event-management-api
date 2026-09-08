using EventManagement.Application.DTOs.Auth;
using EventManagement.Application.Interfaces;
using EventManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Application.Services;

public class AuthService : IAuthService
{
    private readonly IEventDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly IRefreshTokenService _refreshTokenService;

    public AuthService(
        IEventDbContext context,
        IPasswordHasher passwordHasher,
        IJwtService jwtService,
        IRefreshTokenService refreshTokenService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _refreshTokenService = refreshTokenService;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var email = dto.Email.Trim().ToLowerInvariant();
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        if (existingUser != null)
        {
            throw new InvalidOperationException(
                "A user with this email already exists.");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordHash = _passwordHasher.Hash(dto.Password),
            Role = "User"
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return new AuthResponseDto
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var email = dto.Email.Trim().ToLowerInvariant();

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        var isPasswordValid = _passwordHasher.Verify(
            dto.Password,
            user.PasswordHash);

        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        var accessToken = _jwtService.GenerateToken(user);

        var refreshToken = _refreshTokenService.GenerateToken();

        var refreshTokenHash =
            _refreshTokenService.HashToken(refreshToken);

        var session = new UserSession
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            RefreshTokenHash = refreshTokenHash,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };

        _context.UserSessions.Add(session);

        await _context.SaveChangesAsync();

        return new AuthResponseDto
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<AuthResponseDto> RefreshTokenAsync(
        string refreshToken)
    {
        var refreshTokenHash =
            _refreshTokenService.HashToken(refreshToken);

        var session = await _context.UserSessions
            .Include(s => s.User)
            .FirstOrDefaultAsync(
                s => s.RefreshTokenHash == refreshTokenHash);

        if (session == null)
        {
            throw new UnauthorizedAccessException(
                "Invalid refresh token.");
        }

        if (session.RevokedAt != null)
        {
            throw new UnauthorizedAccessException(
                "Refresh token has been revoked.");
        }

        if (session.ExpiresAt <= DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException(
                "Refresh token has expired.");
        }

        var accessToken =
            _jwtService.GenerateToken(session.User);

        return new AuthResponseDto
        {
            Id = session.User.Id,
            Email = session.User.Email,
            Role = session.User.Role,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<AuthResponseDto> LogoutAsync(string refreshToken)
    {
        var hash = _refreshTokenService.HashToken(refreshToken);

        var session = await _context.UserSessions
            .Include(s => s.User)
            .FirstOrDefaultAsync(s =>
                s.RefreshTokenHash == hash);

        if (session == null)
        {
            throw new UnauthorizedAccessException(
                "Invalid refresh token.");
        }

        session.RevokedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new AuthResponseDto
        {
            Id = session.User.Id,
            Email = session.User.Email,
            Role = session.User.Role
        };
    }

    public async Task LogoutAllAsync(Guid userId)
    {
        var sessions = await _context.UserSessions
            .Where(s =>
                s.UserId == userId &&
                s.RevokedAt == null)
            .ToListAsync();

        foreach (var session in sessions)
        {
            session.RevokedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
    }
}
