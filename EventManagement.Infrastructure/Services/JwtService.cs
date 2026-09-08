using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EventManagement.Application.Interfaces;
using EventManagement.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EventManagement.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly SymmetricSecurityKey _signingKey;

    public JwtService(IConfiguration configuration, SymmetricSecurityKey signingKey)
    {
        _configuration = configuration;
        _signingKey = signingKey;
    }

    public string GenerateToken(User user)
    {
        var jwtSettings = _configuration.GetSection("Jwt");

        var issuer = jwtSettings["Issuer"]
            ?? throw new InvalidOperationException("JWT issuer is not configured.");

        var audience = jwtSettings["Audience"]
            ?? throw new InvalidOperationException("JWT audience is not configured.");

        var expirationMinutes = int.Parse(
            jwtSettings["ExpirationMinutes"] ?? "60");

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(ClaimTypes.Role, user.Role)
        };

        var credentials = new SigningCredentials(
            _signingKey,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
