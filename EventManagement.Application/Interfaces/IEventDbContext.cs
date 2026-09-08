using EventManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Application.Interfaces;

public interface IEventDbContext
{
    DbSet<Event> Events { get; }
    DbSet<User> Users { get; }
    DbSet<UserSession> UserSessions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
