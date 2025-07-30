namespace EventPlannerAPI.Models
{
    public class Rsvp
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Username { get; set; } = string.Empty;
        public DateTime RsvpDate { get; set; } = DateTime.UtcNow;
    }
}
