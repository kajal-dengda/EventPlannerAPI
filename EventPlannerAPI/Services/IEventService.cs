using EventPlannerAPI.Models;
using EventPlannerAPI.Models.DTOs;

namespace EventPlannerAPI.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<IEnumerable<Event>> GetEventsByUserAsync(string username);
        Task<Event?> GetEventByIdAsync(int id);
        Task<Event> CreateEventAsync(CreateEventDto eventDto, string username);
        Task<Event?> UpdateEventAsync(int id, UpdateEventDto eventDto, string username);
        Task<bool> DeleteEventAsync(int id, string username);
        Task<bool> RsvpToEventAsync(int eventId, string username);
        Task<bool> HasUserRsvpedAsync(int eventId, string username);
    }
}
