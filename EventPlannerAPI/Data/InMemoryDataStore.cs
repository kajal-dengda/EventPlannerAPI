using EventPlannerAPI.Models;

namespace EventPlannerAPI.Data
{
    public class InMemoryDataStore
    {
        private static readonly List<Event> _events = new();
        private static readonly List<User> _users = new();
        private static readonly List<Rsvp> _rsvps = new();
        private static int _eventIdCounter = 1;
        private static int _rsvpIdCounter = 1;

        public List<Event> Events => _events;
        public List<User> Users => _users;
        public List<Rsvp> Rsvps => _rsvps;

        public int GetNextEventId() => _eventIdCounter++;
        public int GetNextRsvpId() => _rsvpIdCounter++;
    }
}
