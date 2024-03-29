﻿namespace Homies.ViewModels.Event
{
    public class EventDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Type { get; set; } = null!;

        public string Organiser { get; set; } = null!;
    }
}