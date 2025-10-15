# Stage 2 - Data Structures and Indexing

## What was added in this stage

### Files Created:
1. **Data/Structures.cs** (~320 lines) - Advanced data structures implementation
   - **EventIndexes** static class - manages all indexes
   - **EventPriorityQueue** class - custom priority queue for popularity-based sorting
   - **EventPriorityItem** - wrapper for priority queue items
   - **EventPriorityComparer** - custom comparer for the priority queue

### Files Modified:
1. **EventStore.cs**
   - Added `using MunicipalServiceApp.Data;`
   - Added `RebuildIndexes()` method
   - Calls `RebuildIndexes()` in static constructor after loading events
   - Updated `AddEvent()` to rebuild indexes when new events are added

2. **MunicipalServiceApp.csproj**
   - Added reference to `Data\Structures.cs`

## Advanced Data Structures Implemented

### 1. SortedDictionary<DateTime, List<Event>>
**Purpose:** Fast date-range queries  
**Key Benefit:** O(log n) lookup by date, automatically maintains sorted order  
**Usage:** `EventIndexes.EventsByDate`

```csharp
// Events organized by date (sorted)
// Key: DateTime (date only), Value: List of events on that date
SortedDictionary<DateTime, List<Event>> EventsByDate
```

**How it helps:**
- Searching events in a date range is much faster
- Can stop early when searching (since sorted)
- Automatic chronological ordering

### 2. Dictionary<string, List<Event>>
**Purpose:** O(1) category filtering  
**Key Benefit:** Instant lookup by category name  
**Usage:** `EventIndexes.EventsByCategory`

```csharp
// Events indexed by category
// Key: Category name, Value: All events in that category
Dictionary<string, List<Event>> EventsByCategory
```

**How it helps:**
- Filter by category in constant time
- No need to scan all events

### 3. HashSet<string>
**Purpose:** Track unique categories  
**Key Benefit:** O(1) lookup and duplicate prevention  
**Usage:** `EventIndexes.UniqueCategories`

```csharp
// All unique category names
HashSet<string> UniqueCategories
```

**How it helps:**
- Populate category dropdown instantly
- Check if category exists quickly
- Automatic duplicate prevention

### 4. Custom PriorityQueue (EventPriorityQueue)
**Purpose:** Keep events ordered by popularity for recommendations  
**Implementation:** Uses SortedSet with custom comparer (max-heap behavior)  
**Key Benefit:** Always have top N popular events ready

```csharp
public class EventPriorityQueue
{
    public void Enqueue(Event evt)           // Add event
    public Event Dequeue()                   // Get most popular
    public Event Peek()                      // View most popular
    public List<Event> GetTopN(int n)       // Get top N popular
}
```

**How it helps:**
- Generate recommendations instantly (already sorted by popularity)
- No need to sort entire list each time
- Efficient updates when popularity changes

### 5. Stack<string>
**Purpose:** Track search history (LIFO - Last In First Out)  
**Usage:** `EventIndexes.SearchHistory`

```csharp
Stack<string> SearchHistory
```

**How it helps:**
- Most recent searches on top
- Can implement "undo" search functionality
- Limited to last 20 searches to save memory

### 6. Queue<Event>
**Purpose:** Pending announcements (FIFO - First In First Out)  
**Usage:** `EventIndexes.PendingAnnouncements`

```csharp
Queue<Event> PendingAnnouncements
```

**How it helps:**
- Process upcoming events in order
- Fair processing (first announced, first notified)
- Demonstrates FIFO vs LIFO difference

## Key Methods

### BuildIndexes(List<Event> events)
Builds all indexes from scratch. Called when:
- App starts (after loading events)
- New event is added
- Events are modified

### GetEventsByDateRange(DateTime from, DateTime to)
Efficiently searches events in a date range using the SortedDictionary.

### GetEventsByCategory(string category)
O(1) lookup of all events in a category using the Dictionary.

### RecordSearchQuery(string query)
Pushes search query onto the Stack for history tracking.

### GetIndexStats()
Returns diagnostic string showing counts in each data structure.

## How the Indexes Work Together

```
┌─────────────────────────────────────────────────────┐
│  EventStore loads events.xml                       │
│  ↓                                                  │
│  Calls RebuildIndexes()                           │
│  ↓                                                  │
│  EventIndexes.BuildIndexes(events)                │
│  ├─→ Populates EventsByDate (SortedDictionary)   │
│  ├─→ Populates EventsByCategory (Dictionary)     │
│  ├─→ Populates UniqueCategories (HashSet)        │
│  ├─→ Populates PopularEvents (PriorityQueue)     │
│  ├─→ Populates PendingAnnouncements (Queue)      │
│  └─→ SearchHistory (Stack) tracks searches       │
└─────────────────────────────────────────────────────┘
```

## Example Usage

```csharp
// Fast date-range search
var events = EventIndexes.GetEventsByDateRange(
    DateTime.Now, 
    DateTime.Now.AddDays(7)
);

// Fast category filter
var sportsEvents = EventIndexes.GetEventsByCategory("Sports");

// Get top 3 most popular events for recommendations
var topEvents = EventIndexes.PopularEvents.GetTopN(3);

// Record a search
EventIndexes.RecordSearchQuery("community clean-up");

// Get recent searches
var recent = EventIndexes.GetRecentSearches(5);

// Check diagnostic info
string stats = EventIndexes.GetIndexStats();
```

## Performance Benefits

### Without Indexes (Linear Search):
- Date range query: O(n) - scan all events
- Category filter: O(n) - scan all events
- Top 5 popular: O(n log n) - sort all events

### With Indexes:
- Date range query: O(log n + k) - binary search + k results
- Category filter: O(1) - direct dictionary lookup
- Top 5 popular: O(1) - already sorted in priority queue

**For 10,000 events:**
- Linear date search: ~10,000 comparisons
- Indexed date search: ~13 comparisons (log₂ 10000) + results
- **~770x faster!**

## Testing This Stage

1. Build the project
2. Run the app
3. Open Local Events form
4. The indexes are built automatically in background

**To verify indexes are working:**
- Add a breakpoint in `EventIndexes.BuildIndexes()`
- Run in debug mode
- Check Watch window:
  - `EventIndexes.EventsByDate.Count` - should show number of unique dates
  - `EventIndexes.UniqueCategories.Count` - should show 5-6 categories
  - `EventIndexes.PopularEvents.Count` - should show 10 events

**Console test (add to Program.cs Main if needed):**
```csharp
// After EventStore initializes
Console.WriteLine(EventIndexes.GetIndexStats());
```

## What's Ready for Next Stage

✅ All indexes built and ready  
✅ Fast search methods available  
✅ Priority queue ready for recommendations  
✅ Search history tracking in place  

## What's Coming in Next Stage

🔜 Wire indexes to LocalEventsForm search functionality  
🔜 Implement keyword + category + date range filtering  
🔜 Build recommendation engine using search history  
🔜 Add diagnostics form to visualize data structures  

## Notes for Developers

- **Why SortedDictionary?** Built-in red-black tree, O(log n) operations, maintains sort order
- **Why not just sort a List?** Sorting on every search is O(n log n), indexes are built once
- **Why custom PriorityQueue?** .NET Framework 4.7.2 doesn't have one, .NET 6+ does
- **Stack vs Queue?** Stack = LIFO (recent searches), Queue = FIFO (fair announcements)

The data structures chosen match the assignment requirements while providing real performance benefits for searching and recommendations.

---

## Commit this stage:

```bash
git add Data/Structures.cs EventStore.cs MunicipalServiceApp.csproj STAGE_2_README.md
git commit -m "feat(data): build in-memory indexes (sorted dictionary, dictionary, priority queue, sets)"
```

---

Ready for Stage 3 - implementing the search functionality!

