using EventManagement.Api.Data;
using EventManagement.Api.Models;
using EventManagement.DTOs.Events;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Api.Services;

public class EventService : IEventService
{
    private readonly EventDbContext _context;

    public EventService(EventDbContext context)
    {
        _context = context;
    }
    private EventDto MapToDto(Event eventItem)
    {
        return new EventDto
        {
            Id = eventItem.Id,
            Name = eventItem.Name,
            Description = eventItem.Description,
            StartDate = eventItem.StartDate,
            EndDate = eventItem.EndDate,
            Location = eventItem.Location
        };
    }
    public async Task<IEnumerable<EventDto>> GetEventsAsync()
    {
        return await _context.Events
            .Select(e => new EventDto
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Location = e.Location
            })
            .ToListAsync();
    }
    public async Task<EventDto?> GetEventAsync(Guid id)
    {
        var eventItem = await _context.Events.FindAsync(id);

        if (eventItem == null)
        {
            return null;
        }

        return MapToDto(eventItem);
    }
    public async Task<EventDto> CreateEventAsync(CreateEventDto dto)
    {
        var eventItem = new Event
        {
            Name = dto.Name,
            Description = dto.Description,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Location = dto.Location
        };

        _context.Events.Add(eventItem);

        await _context.SaveChangesAsync();

        return MapToDto(eventItem);
    }
    public async Task<EventDto?> UpdateEventAsync(Guid id, UpdateEventDto dto)
    {
        var existingEvent = await _context.Events.FindAsync(id);

        if (existingEvent == null)
        {
            return null;
        }

        existingEvent.Name = dto.Name;
        existingEvent.Description = dto.Description;
        existingEvent.StartDate = dto.StartDate;
        existingEvent.EndDate = dto.EndDate;
        existingEvent.Location = dto.Location;

        await _context.SaveChangesAsync();

        return MapToDto(existingEvent);
    }
    public async Task<bool> DeleteEventAsync(Guid id)
    {
        var eventItem = await _context.Events.FindAsync(id);

        if (eventItem == null)
        {
            return false;
        }

        _context.Events.Remove(eventItem);

        await _context.SaveChangesAsync();

        return true;
    }
}