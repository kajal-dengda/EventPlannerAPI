using Microsoft.AspNetCore.Mvc;
using EventPlannerAPI.Models.DTOs;
using EventPlannerAPI.Services;

namespace EventPlannerAPI.Controllers
{
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
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("my-events/{username}")]
        public async Task<IActionResult> GetMyEvents(string username)
        {
            var events = await _eventService.GetEventsByUserAsync(username);
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var eventItem = await _eventService.GetEventByIdAsync(id);
            if (eventItem == null)
                return NotFound();

            return Ok(eventItem);
        }

        [HttpPost("{username}")]
        public async Task<IActionResult> CreateEvent(string username, [FromBody] CreateEventDto eventDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newEvent = await _eventService.CreateEventAsync(eventDto, username);
            return CreatedAtAction(nameof(GetEventById), new { id = newEvent.Id }, newEvent);
        }

        [HttpPut("{id}/{username}")]
        public async Task<IActionResult> UpdateEvent(int id, string username, [FromBody] UpdateEventDto eventDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedEvent = await _eventService.UpdateEventAsync(id, eventDto, username);
            if (updatedEvent == null)
                return NotFound("Event not found or you don't have permission to update it");

            return Ok(updatedEvent);
        }

        [HttpDelete("{id}/{username}")]
        public async Task<IActionResult> DeleteEvent(int id, string username)
        {
            var result = await _eventService.DeleteEventAsync(id, username);
            if (!result)
                return NotFound("Event not found or you don't have permission to delete it");

            return NoContent();
        }

        [HttpPost("{eventId}/rsvp/{username}")]
        public async Task<IActionResult> RsvpToEvent(int eventId, string username)
        {
            var result = await _eventService.RsvpToEventAsync(eventId, username);
            if (!result)
                return BadRequest("Unable to RSVP - event may be full or you've already RSVP'd");

            return Ok(new { message = "RSVP successful" });
        }

        [HttpGet("{eventId}/rsvp-status/{username}")]
        public async Task<IActionResult> GetRsvpStatus(int eventId, string username)
        {
            var hasRsvped = await _eventService.HasUserRsvpedAsync(eventId, username);
            return Ok(new { hasRsvped });
        }
    }
}