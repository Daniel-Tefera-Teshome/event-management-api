using EventManagement.Application.DTOs.Events;
using EventManagement.Application.Interfaces;
using EventManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EventManagement.Application.Services;

public class EventService : IEventService
{
    private readonly IEventDbContext _context;
    private readonly ILogger<EventService> _logger;

    public EventService(
        IEventDbContext context,
        ILogger<EventService> logger)
    {
        _context = context;
        _logger = logger;
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
            Description = dto.Description ?? string.Empty,
            StartDate = dto.StartDate!.Value,
            EndDate = dto.EndDate!.Value,
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
        _logger.LogInformation(
            "Creating event with name: {EventName}",
            dto.Name);

        var eventItem = MapToEntity(dto);

        _context.Events.Add(eventItem);

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Event created successfully with ID: {EventId}",
            eventItem.Id);

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
        existingEvent.Description = dto.Description ?? string.Empty;
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
