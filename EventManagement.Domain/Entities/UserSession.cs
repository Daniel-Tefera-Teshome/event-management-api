namespace EventManagement.Domain.Entities;

public class UserSession
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string RefreshTokenHash { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    public string? DeviceInfo { get; set; }

    public string? IpAddress { get; set; }

    public User User { get; set; } = null!;
}
