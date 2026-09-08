using EventManagement.Application.DTOs.Events;

namespace EventManagement.Application.Interfaces;

public interface IEventService
{
    Task<IEnumerable<EventDto>> GetEventsAsync();

    Task<EventDto?> GetEventAsync(Guid id);

    Task<EventDto> CreateEventAsync(CreateEventDto dto);

    Task<EventDto?> UpdateEventAsync(Guid id, UpdateEventDto dto);

    Task<bool> DeleteEventAsync(Guid id);
}
