﻿namespace EventPlannerAPI.Models.DTOs
{
    public class CreateEventDto
    {
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int MaxRsvpCount { get; set; }
    }
}
