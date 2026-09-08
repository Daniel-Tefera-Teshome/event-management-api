namespace EventManagement.Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string Role { get; set; } = "User";

    public ICollection<UserSession> Sessions { get; set; } = new List<UserSession>();
}
