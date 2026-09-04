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
    private static EventDto MapToDto(Event eventItem)
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
    private static Event MapToEntity(CreateEventDto dto)
    {
        return new Event
        {
            Name = dto.Name,
            Description = dto.Description,
            StartDate = dto.StartDate.Value,
            EndDate = dto.EndDate.Value,
            Location = dto.Location
        };
    }
    public async Task<IEnumerable<EventDto>> GetEventsAsync()
    {
        return await _context.Events
            .Select(e => MapToDto(e))
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
        var eventItem = MapToEntity(dto);

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