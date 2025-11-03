using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using MunicipalServiceApp.Models;
using MunicipalServiceApp.Data;

namespace MunicipalServiceApp
{
    /// <summary>
    /// Manages event data storage and retrieval.
    /// Handles both events and search history persistence using XML files.
    /// Also builds in-memory indexes for fast searching.
    /// </summary>
    public static class EventStore
    {
        private static readonly string DataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        private static readonly string EventsFilePath = Path.Combine(DataFolder, "events.xml");
        private static readonly string SearchHistoryFilePath = Path.Combine(DataFolder, "searchHistory.xml");

        // In-memory storage for events
        public static List<Event> Events { get; private set; } = new List<Event>();

        // Track user search queries for recommendations
        public static List<string> SearchHistory { get; private set; } = new List<string>();

        static EventStore()
        {
            // Make sure Data folder exists
            if (!Directory.Exists(DataFolder))
                Directory.CreateDirectory(DataFolder);

            LoadEvents();
            LoadSearchHistory();

            // If no events exist, seed some sample data
            if (Events.Count == 0)
            {
                SeedSampleEvents();
            }

            // Build indexes after loading events for fast searching
            RebuildIndexes();
        }

        /// <summary>
        /// Loads events from the XML file.
        /// If file doesn't exist, starts with empty list.
        /// </summary>
        public static void LoadEvents()
        {
            if (File.Exists(EventsFilePath))
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(List<Event>));
                    using (var reader = new StreamReader(EventsFilePath))
                    {
                        Events = (List<Event>)serializer.Deserialize(reader);
                    }
                }
                catch (Exception ex)
                {
                    // If loading fails, start fresh (maybe file was corrupted)
                    Events = new List<Event>();
                    System.Diagnostics.Debug.WriteLine($"Error loading events: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Saves all events to XML file.
        /// </summary>
        public static void SaveEvents()
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<Event>));
                using (var writer = new StreamWriter(EventsFilePath))
                {
                    serializer.Serialize(writer, Events);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving events: {ex.Message}");
                throw; // Re-throw so UI can show error
            }
        }

        /// <summary>
        /// Adds a new event to the store and saves it.
        /// Also rebuilds indexes to include the new event.
        /// </summary>
        public static void AddEvent(Event evt)
        {
            Events.Add(evt);
            SaveEvents();
            RebuildIndexes(); // Rebuild indexes with new event
        }

        /// <summary>
        /// Rebuilds all data structure indexes.
        /// Call this after modifying events or loading new data.
        /// </summary>
        public static void RebuildIndexes()
        {
            EventIndexes.BuildIndexes(Events);
        }

        /// <summary>
        /// Loads search history from XML file.
        /// </summary>
        public static void LoadSearchHistory()
        {
            if (File.Exists(SearchHistoryFilePath))
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(List<string>));
                    using (var reader = new StreamReader(SearchHistoryFilePath))
                    {
                        SearchHistory = (List<string>)serializer.Deserialize(reader);
                    }
                }
                catch
                {
                    SearchHistory = new List<string>();
                }
            }
        }

        /// <summary>
        /// Saves search history to XML file.
        /// </summary>
        public static void SaveSearchHistory()
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<string>));
                using (var writer = new StreamWriter(SearchHistoryFilePath))
                {
                    serializer.Serialize(writer, SearchHistory);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving search history: {ex.Message}");
            }
        }

        /// <summary>
        /// Searches events using indexes for efficiency.
        /// Combines keyword, category, and date range filtering.
        /// </summary>
        public static List<Event> SearchEvents(string keyword, string category, DateTime? fromDate, DateTime? toDate)
        {
            // Start with all events
            List<Event> results = new List<Event>(Events);

            System.Diagnostics.Debug.WriteLine($"Search started: Total events={Events.Count}");

            // Step 1: Filter by date range using SortedDictionary (if dates provided)
            if (fromDate.HasValue && toDate.HasValue)
            {
                System.Diagnostics.Debug.WriteLine($"Date filter: {fromDate.Value:yyyy-MM-dd} to {toDate.Value:yyyy-MM-dd}");
                results = EventIndexes.GetEventsByDateRange(fromDate.Value, toDate.Value);
                System.Diagnostics.Debug.WriteLine($"After date filter: {results.Count} events");
            }

            // Step 2: Filter by category using Dictionary (if category specified)
            if (!string.IsNullOrEmpty(category) && category != "All Categories")
            {
                System.Diagnostics.Debug.WriteLine($"Category filter: {category}");
                if (fromDate.HasValue && toDate.HasValue && results.Count > 0)
                {
                    // If we already filtered by date, further filter those results
                    results = results.Where(e => e.Category == category).ToList();
                }
                else if (!fromDate.HasValue || !toDate.HasValue)
                {
                    // Direct category lookup from index
                    results = EventIndexes.GetEventsByCategory(category);
                }
                System.Diagnostics.Debug.WriteLine($"After category filter: {results.Count} events");
            }

            // Step 3: Filter by keyword (search in title and description)
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string lowerKeyword = keyword.ToLower().Trim();
                System.Diagnostics.Debug.WriteLine($"Keyword filter: {lowerKeyword}");
                results = results.Where(e =>
                    e.Title.ToLower().Contains(lowerKeyword) ||
                    e.Description.ToLower().Contains(lowerKeyword) ||
                    e.Location.ToLower().Contains(lowerKeyword)
                ).ToList();

                System.Diagnostics.Debug.WriteLine($"After keyword filter: {results.Count} events");

                // Record this search for recommendations
                RecordSearch(lowerKeyword);
            }

            // Also record category searches for recommendations
            if (!string.IsNullOrEmpty(category) && category != "All Categories")
            {
                RecordSearch(category.ToLower());
            }

            System.Diagnostics.Debug.WriteLine($"Search complete: Returning {results.Count} events");
            return results;
        }

        /// <summary>
        /// Generates event recommendations based on user's search history.
        /// Uses keyword frequency and category matching to score events.
        /// </summary>
        public static List<Event> GetRecommendations()
        {
            if (SearchHistory.Count == 0)
            {
                // No search history - return most popular events
                return EventIndexes.PopularEvents.GetTopN(3);
            }

            // Calculate keyword frequency from search history
            var keywordFrequency = new Dictionary<string, int>();
            foreach (var query in SearchHistory)
            {
                var words = query.Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    string cleanWord = word.ToLower().Trim();
                    if (cleanWord.Length > 2) // Ignore very short words
                    {
                        if (keywordFrequency.ContainsKey(cleanWord))
                            keywordFrequency[cleanWord]++;
                        else
                            keywordFrequency[cleanWord] = 1;
                    }
                }
            }

            // Score each event based on keyword matches
            var scoredEvents = new List<(Event evt, int score)>();

            foreach (var evt in Events)
            {
                int score = evt.PopularityScore; // Start with popularity base score

                // Add points for keyword matches in title (higher weight)
                foreach (var kvp in keywordFrequency)
                {
                    if (evt.Title.ToLower().Contains(kvp.Key))
                        score += kvp.Value * 5; // Title match worth 5x keyword frequency

                    if (evt.Description.ToLower().Contains(kvp.Key))
                        score += kvp.Value * 2; // Description match worth 2x

                    if (evt.Category.ToLower().Contains(kvp.Key))
                        score += kvp.Value * 3; // Category match worth 3x
                }

                scoredEvents.Add((evt, score));
            }

            // Return top 3 events by score
            return scoredEvents
                .OrderByDescending(x => x.score)
                .Take(3)
                .Select(x => x.evt)
                .ToList();
        }

        /// <summary>
        /// Records a search query for recommendation purposes.
        /// Keeps only the last 50 searches to avoid bloat.
        /// </summary>
        public static void RecordSearch(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return;

            SearchHistory.Add(query.ToLower().Trim());
            EventIndexes.RecordSearchQuery(query); // Also record in index Stack

            // Keep only recent searches (last 50)
            if (SearchHistory.Count > 50)
                SearchHistory.RemoveAt(0);

            SaveSearchHistory();
        }

        /// <summary>
        /// Creates sample events for testing and demonstration.
        /// This runs automatically if no events file exists.
        /// </summary>
        private static void SeedSampleEvents()
        {
            Events.Clear();

            // Adding some varied events across different categories and dates
            Events.Add(new Event(
                "Community Clean-Up Day",
                DateTime.Now.AddDays(5),
                "Community",
                "Join us for a neighborhood clean-up event. Bring gloves and bags!",
                "Central Park"
            ));

            Events.Add(new Event(
                "Summer Music Festival",
                DateTime.Now.AddDays(15),
                "Culture",
                "Three-day outdoor music festival featuring local artists and food vendors.",
                "Municipal Stadium"
            ));

            Events.Add(new Event(
                "Youth Soccer Tournament",
                DateTime.Now.AddDays(7),
                "Sports",
                "Annual youth soccer tournament for ages 8-16. Registration required.",
                "Sports Complex"
            ));

            Events.Add(new Event(
                "Farmers Market Opening",
                DateTime.Now.AddDays(2),
                "Community",
                "Weekly farmers market starts this weekend! Fresh produce and local crafts.",
                "Town Square"
            ));

            Events.Add(new Event(
                "Public Safety Workshop",
                DateTime.Now.AddDays(10),
                "Education",
                "Learn about emergency preparedness and community safety resources.",
                "Community Center"
            ));

            Events.Add(new Event(
                "Art Gallery Exhibition",
                DateTime.Now.AddDays(20),
                "Culture",
                "Showcasing local artists' works. Opening night reception included.",
                "Municipal Art Gallery"
            ));

            Events.Add(new Event(
                "Marathon Training Program",
                DateTime.Now.AddDays(3),
                "Sports",
                "Free 12-week marathon training program for beginners and experienced runners.",
                "Riverside Trail"
            ));

            Events.Add(new Event(
                "Town Hall Meeting",
                DateTime.Now.AddDays(12),
                "Government",
                "Monthly town hall meeting to discuss municipal projects and concerns.",
                "City Hall"
            ));

            Events.Add(new Event(
                "Children's Book Fair",
                DateTime.Now.AddDays(18),
                "Education",
                "Family-friendly book fair with author readings and activities for kids.",
                "Public Library"
            ));

            Events.Add(new Event(
                "Street Food Festival",
                DateTime.Now.AddDays(25),
                "Culture",
                "Taste dishes from around the world at our annual street food festival.",
                "Main Street"
            ));

            // Give some events higher popularity to test recommendations
            Events[1].PopularityScore = 15; // Music Festival is popular
            Events[5].PopularityScore = 12; // Art Gallery too
            Events[9].PopularityScore = 10; // Food Festival

            SaveEvents();
        }
    }
}

