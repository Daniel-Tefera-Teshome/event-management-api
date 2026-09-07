using EventManagement.Api.Services;
using EventManagement.DTOs.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EventManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetEvents()
    {
        var events = await _eventService.GetEventsAsync();

        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EventDto>> GetEvent(Guid id)
    {
        var eventItem = await _eventService.GetEventAsync(id);

        if (eventItem == null)
        {
            return NotFound();
        }

        return Ok(eventItem);
    }

    [HttpPost]
    [Authorize]
    // [Authorize(Roles = "Admin")]
    public async Task<ActionResult<EventDto>> CreateEvent(
        CreateEventDto dto)
    {
        var result = await _eventService.CreateEventAsync(dto);

        return CreatedAtAction(
            nameof(GetEvent),
            new { id = result.Id },
            result
        );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EventDto>> UpdateEvent(
        Guid id,
        UpdateEventDto dto)
    {
        var result = await _eventService.UpdateEventAsync(id, dto);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        var deleted = await _eventService.DeleteEventAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}