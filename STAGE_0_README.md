# Stage 0 - Project Skeleton & Sample Data

## What was added in this stage

### Files Created:
1. **Models/Event.cs** - Event model class with all required properties
   - Id, Title, Date, Category, Description, Location, PopularityScore
   - Methods for incrementing popularity (used for recommendations later)
   - Parameterless constructor for XML serialization

2. **EventStore.cs** - Data management for events and search history
   - LoadEvents() / SaveEvents() - XML persistence
   - LoadSearchHistory() / SaveSearchHistory() - track user searches
   - AddEvent() - add new events
   - RecordSearch() - track searches for recommendations
   - SeedSampleEvents() - creates 10 sample events if none exist

### Sample Data Included:
The EventStore automatically creates 10 diverse events when first run:
- Community events (Clean-up, Farmers Market)
- Cultural events (Music Festival, Art Gallery, Food Festival)
- Sports events (Soccer, Marathon)
- Educational events (Safety Workshop, Book Fair)
- Government events (Town Hall Meeting)

Some events have pre-set popularity scores to test recommendation features later.

## Testing Stage 0

To verify this stage works:
1. Build the project (Ctrl+Shift+B in Visual Studio)
2. The EventStore static constructor will run on first use
3. Check `bin/Debug/Data/events.xml` - should contain 10 sample events
4. You can peek at the data structure in the XML file

## Next Stage Preview

Stage 1 will add:
- Data structure implementations (SortedDictionary, PriorityQueue wrapper, etc.)
- The LocalEventsForm UI
- Search functionality

---

## Commit this stage:

```bash
git add Models/Event.cs EventStore.cs STAGE_0_README.md
git commit -m "chore: add Event model and EventStore skeleton with sample data"
```

Then proceed to Stage 1 when ready.

