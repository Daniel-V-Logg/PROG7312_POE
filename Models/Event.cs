using System;

namespace MunicipalServiceApp.Models
{
    /// <summary>
    /// Represents a local event or announcement in the municipality.
    /// Used to store event details and track popularity for recommendations.
    /// </summary>
    public class Event
    {
        // Unique identifier for the event
        public string Id { get; set; }

        // Event title/name
        public string Title { get; set; }

        // When the event happens
        public DateTime Date { get; set; }

        // Category like "Community", "Sports", "Culture", etc.
        public string Category { get; set; }

        // Short description of the event
        public string Description { get; set; }

        // Where the event takes place
        public string Location { get; set; }

        // Tracks how popular this event is (based on views/searches)
        // Higher score = more popular, used for recommendations
        public int PopularityScore { get; set; }

        // Parameterless constructor needed for XML serialization
        public Event()
        {
            Id = Guid.NewGuid().ToString();
            PopularityScore = 0;
        }

        // Constructor with all fields
        public Event(string title, DateTime date, string category, string description, string location)
        {
            Id = Guid.NewGuid().ToString();
            Title = title;
            Date = date;
            Category = category;
            Description = description;
            Location = location;
            PopularityScore = 0;
        }

        /// <summary>
        /// Increases the popularity score when someone searches for or views this event.
        /// </summary>
        public void IncrementPopularity()
        {
            PopularityScore++;
        }

        public override string ToString()
        {
            return $"{Title} - {Date:dd/MM/yyyy} ({Category})";
        }
    }
}

