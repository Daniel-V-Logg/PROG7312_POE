using System;
using System.Collections.Generic;
using System.Linq;
using MunicipalServiceApp.Models;

namespace MunicipalServiceApp.Data
{
    /// <summary>
    /// Advanced data structures for efficient event searching and recommendations.
    /// Demonstrates use of SortedDictionary, Dictionary, HashSet, and custom PriorityQueue.
    /// </summary>
    public static class EventIndexes
    {
        // SortedDictionary organizes events by date for fast date-range queries
        // Key: Date (sorted), Value: List of events on that date
        public static SortedDictionary<DateTime, List<Event>> EventsByDate { get; private set; }

        // Dictionary maps category names to events for O(1) category filtering
        public static Dictionary<string, List<Event>> EventsByCategory { get; private set; }

        // HashSet stores all unique categories for quick lookups and dropdowns
        public static HashSet<string> UniqueCategories { get; private set; }

        // PriorityQueue keeps events ordered by popularity (most popular first)
        // Used for generating recommendations
        public static EventPriorityQueue PopularEvents { get; private set; }

        // Stack to track recent search operations (for undo/history features)
        public static Stack<string> SearchHistory { get; private set; }

        // Queue for processing incoming event announcements (FIFO)
        public static Queue<Event> PendingAnnouncements { get; private set; }

        static EventIndexes()
        {
            // Initialize all data structures
            EventsByDate = new SortedDictionary<DateTime, List<Event>>();
            EventsByCategory = new Dictionary<string, List<Event>>();
            UniqueCategories = new HashSet<string>();
            PopularEvents = new EventPriorityQueue();
            SearchHistory = new Stack<string>();
            PendingAnnouncements = new Queue<Event>();
        }

        /// <summary>
        /// Builds all indexes from the provided event list.
        /// Call this after loading events or when events are modified.
        /// </summary>
        public static void BuildIndexes(List<Event> events)
        {
            // Clear existing indexes
            ClearIndexes();

            foreach (var evt in events)
            {
                // Index by date (SortedDictionary)
                // Using date-only key (normalize to midnight) for grouping
                var dateKey = evt.Date.Date;
                if (!EventsByDate.ContainsKey(dateKey))
                {
                    EventsByDate[dateKey] = new List<Event>();
                }
                EventsByDate[dateKey].Add(evt);

                // Index by category (Dictionary)
                if (!EventsByCategory.ContainsKey(evt.Category))
                {
                    EventsByCategory[evt.Category] = new List<Event>();
                }
                EventsByCategory[evt.Category].Add(evt);

                // Track unique categories (HashSet)
                UniqueCategories.Add(evt.Category);

                // Add to priority queue (ordered by popularity)
                PopularEvents.Enqueue(evt);

                // Simulate adding to pending announcements queue
                // (In real app, this would be for newly announced events)
                if (evt.Date > DateTime.Now && evt.Date <= DateTime.Now.AddDays(7))
                {
                    PendingAnnouncements.Enqueue(evt);
                }
            }
        }

        /// <summary>
        /// Clears all indexes. Called before rebuilding.
        /// </summary>
        public static void ClearIndexes()
        {
            EventsByDate.Clear();
            EventsByCategory.Clear();
            UniqueCategories.Clear();
            PopularEvents.Clear();
            PendingAnnouncements.Clear();
        }

        /// <summary>
        /// Searches events by date range using the sorted dictionary.
        /// Much faster than linear search through all events.
        /// </summary>
        public static List<Event> GetEventsByDateRange(DateTime fromDate, DateTime toDate)
        {
            var results = new List<Event>();

            // Iterate through sorted dictionary keys in range
            foreach (var kvp in EventsByDate)
            {
                if (kvp.Key >= fromDate.Date && kvp.Key <= toDate.Date)
                {
                    results.AddRange(kvp.Value);
                }
                else if (kvp.Key > toDate.Date)
                {
                    // Since it's sorted, we can stop early
                    break;
                }
            }

            return results;
        }

        /// <summary>
        /// Gets events by category using the dictionary index.
        /// O(1) lookup instead of O(n) filtering.
        /// </summary>
        public static List<Event> GetEventsByCategory(string category)
        {
            if (EventsByCategory.ContainsKey(category))
            {
                return new List<Event>(EventsByCategory[category]);
            }
            return new List<Event>();
        }

        /// <summary>
        /// Records a search query in the history stack.
        /// Demonstrates Stack usage (LIFO - Last In First Out).
        /// </summary>
        public static void RecordSearchQuery(string query)
        {
            if (!string.IsNullOrWhiteSpace(query))
            {
                SearchHistory.Push(query.ToLower().Trim());

                // Keep only last 20 searches to avoid memory bloat
                if (SearchHistory.Count > 20)
                {
                    // Convert to list, remove oldest, convert back
                    var items = SearchHistory.ToList();
                    items.RemoveAt(items.Count - 1);
                    SearchHistory.Clear();
                    for (int i = items.Count - 1; i >= 0; i--)
                    {
                        SearchHistory.Push(items[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the most recent search queries.
        /// Useful for showing search suggestions.
        /// </summary>
        public static List<string> GetRecentSearches(int count = 5)
        {
            return SearchHistory.Take(count).ToList();
        }

        /// <summary>
        /// Processes pending announcements (demonstrates Queue usage - FIFO).
        /// In a real app, this could send notifications for upcoming events.
        /// </summary>
        public static Event GetNextPendingAnnouncement()
        {
            if (PendingAnnouncements.Count > 0)
            {
                return PendingAnnouncements.Dequeue();
            }
            return null;
        }

        /// <summary>
        /// Gets statistics about the indexes (for diagnostics/testing).
        /// </summary>
        public static string GetIndexStats()
        {
            return $"Indexes Summary:\n" +
                   $"- Events by Date: {EventsByDate.Count} unique dates\n" +
                   $"- Events by Category: {EventsByCategory.Count} categories\n" +
                   $"- Unique Categories: {UniqueCategories.Count}\n" +
                   $"- Popular Events Queue: {PopularEvents.Count} events\n" +
                   $"- Search History: {SearchHistory.Count} queries\n" +
                   $"- Pending Announcements: {PendingAnnouncements.Count} events";
        }
    }

    /// <summary>
    /// Custom priority queue implementation for events based on popularity.
    /// .NET Framework 4.7.2 doesn't have PriorityQueue, so we implement one.
    /// Uses a max-heap approach where higher popularity = higher priority.
    /// </summary>
    public class EventPriorityQueue
    {
        // Internal storage using SortedSet for automatic ordering
        // SortedSet maintains items in sorted order by the comparer
        private SortedSet<EventPriorityItem> heap;
        private int insertionOrder; // tie-breaker for events with same priority

        public EventPriorityQueue()
        {
            heap = new SortedSet<EventPriorityItem>(new EventPriorityComparer());
            insertionOrder = 0;
        }

        /// <summary>
        /// Adds an event to the priority queue.
        /// </summary>
        public void Enqueue(Event evt)
        {
            var item = new EventPriorityItem
            {
                Event = evt,
                Priority = evt.PopularityScore,
                InsertionOrder = insertionOrder++
            };
            heap.Add(item);
        }

        /// <summary>
        /// Removes and returns the highest-priority event.
        /// </summary>
        public Event Dequeue()
        {
            if (heap.Count == 0)
                return null;

            var highest = heap.Max; // Get highest priority (max-heap)
            heap.Remove(highest);
            return highest.Event;
        }

        /// <summary>
        /// Returns the highest-priority event without removing it.
        /// </summary>
        public Event Peek()
        {
            if (heap.Count == 0)
                return null;

            return heap.Max.Event;
        }

        /// <summary>
        /// Gets the top N most popular events.
        /// Useful for recommendations.
        /// </summary>
        public List<Event> GetTopN(int n)
        {
            var result = new List<Event>();
            var items = heap.Reverse().Take(n); // Reverse because we want highest first

            foreach (var item in items)
            {
                result.Add(item.Event);
            }

            return result;
        }

        public int Count => heap.Count;

        public void Clear()
        {
            heap.Clear();
            insertionOrder = 0;
        }
    }

    /// <summary>
    /// Wrapper class for events in the priority queue.
    /// Holds the event, its priority, and insertion order for tie-breaking.
    /// </summary>
    internal class EventPriorityItem
    {
        public Event Event { get; set; }
        public int Priority { get; set; }
        public int InsertionOrder { get; set; }
    }

    /// <summary>
    /// Comparer for priority queue items.
    /// Sorts by priority (descending), then by insertion order (ascending).
    /// </summary>
    internal class EventPriorityComparer : IComparer<EventPriorityItem>
    {
        public int Compare(EventPriorityItem x, EventPriorityItem y)
        {
            if (x == null || y == null)
                return 0;

            // First compare by priority (higher priority = comes first)
            int priorityComparison = y.Priority.CompareTo(x.Priority);
            if (priorityComparison != 0)
                return priorityComparison;

            // If priorities are equal, use insertion order (FIFO for same priority)
            return x.InsertionOrder.CompareTo(y.InsertionOrder);
        }
    }
}

