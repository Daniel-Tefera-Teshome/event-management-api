using System.ComponentModel.DataAnnotations;

namespace EventManagement.DTOs.Auth;

public class RefreshTokenDto
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}