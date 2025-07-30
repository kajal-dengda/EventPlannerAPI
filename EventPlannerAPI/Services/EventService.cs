using EventPlannerAPI.Data;
using EventPlannerAPI.Models;
using EventPlannerAPI.Models.DTOs;

namespace EventPlannerAPI.Services
{
    public class EventService : IEventService
    {
        private readonly InMemoryDataStore _dataStore;

        public EventService(InMemoryDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            var events = _dataStore.Events.Where(e => e.Date >= DateTime.Today).ToList();
            return Task.FromResult<IEnumerable<Event>>(events);
        }

        public Task<IEnumerable<Event>> GetEventsByUserAsync(string username)
        {
            var events = _dataStore.Events.Where(e => e.CreatedBy == username).ToList();
            return Task.FromResult<IEnumerable<Event>>(events);
        }

        public Task<Event?> GetEventByIdAsync(int id)
        {
            var eventItem = _dataStore.Events.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(eventItem);
        }

        public Task<Event> CreateEventAsync(CreateEventDto eventDto, string username)
        {
            var newEvent = new Event
            {
                Id = _dataStore.GetNextEventId(),
                Name = eventDto.Name,
                Date = eventDto.Date,
                Time = eventDto.Time,
                Location = eventDto.Location,
                Description = eventDto.Description,
                MaxRsvpCount = eventDto.MaxRsvpCount,
                CreatedBy = username,
                CurrentRsvpCount = 0
            };

            _dataStore.Events.Add(newEvent);
            return Task.FromResult(newEvent);
        }

        public Task<Event?> UpdateEventAsync(int id, UpdateEventDto eventDto, string username)
        {
            var existingEvent = _dataStore.Events.FirstOrDefault(e => e.Id == id && e.CreatedBy == username);
            if (existingEvent == null)
                return Task.FromResult<Event?>(null);

            existingEvent.Name = eventDto.Name;
            existingEvent.Date = eventDto.Date;
            existingEvent.Time = eventDto.Time;
            existingEvent.Location = eventDto.Location;
            existingEvent.Description = eventDto.Description;
            existingEvent.MaxRsvpCount = eventDto.MaxRsvpCount;

            return Task.FromResult<Event?>(existingEvent);
        }

        public Task<bool> DeleteEventAsync(int id, string username)
        {
            var eventToDelete = _dataStore.Events.FirstOrDefault(e => e.Id == id && e.CreatedBy == username);
            if (eventToDelete == null)
                return Task.FromResult(false);

            _dataStore.Events.Remove(eventToDelete);

            // Remove associated RSVPs
            var rsvpsToRemove = _dataStore.Rsvps.Where(r => r.EventId == id).ToList();
            foreach (var rsvp in rsvpsToRemove)
            {
                _dataStore.Rsvps.Remove(rsvp);
            }

            return Task.FromResult(true);
        }

        public Task<bool> RsvpToEventAsync(int eventId, string username)
        {
            var eventItem = _dataStore.Events.FirstOrDefault(e => e.Id == eventId);
            if (eventItem == null || eventItem.CurrentRsvpCount >= eventItem.MaxRsvpCount)
                return Task.FromResult(false);

            // Check if user already RSVP'd
            if (_dataStore.Rsvps.Any(r => r.EventId == eventId && r.Username == username))
                return Task.FromResult(false);

            var rsvp = new Rsvp
            {
                Id = _dataStore.GetNextRsvpId(),
                EventId = eventId,
                Username = username
            };

            _dataStore.Rsvps.Add(rsvp);
            eventItem.CurrentRsvpCount++;
            eventItem.RsvpUsers.Add(username);

            return Task.FromResult(true);
        }

        public Task<bool> HasUserRsvpedAsync(int eventId, string username)
        {
            var hasRsvped = _dataStore.Rsvps.Any(r => r.EventId == eventId && r.Username == username);
            return Task.FromResult(hasRsvped);
        }
    }
}