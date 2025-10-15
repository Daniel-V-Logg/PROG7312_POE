# Debugging Search Issue

## Changes Made

I've added extensive debugging to help identify why search isn't working:

### 1. Debug Output in EventStore.SearchEvents()
- Shows total events at start
- Shows date range being searched
- Shows results after each filter step
- Shows final result count

### 2. Debug Output in LocalEventsForm.btnSearch_Click()
- Shows search parameters before calling search
- Shows result count after search

### 3. Diagnostic Message Box
- If no results found, shows a dialog with:
  - What keyword was used
  - What category was selected
  - What date range was used
  - How many total events exist

## How to Debug

### Option 1: Run in Debug Mode
1. In Visual Studio, press **F5** (Start Debugging)
2. Open Local Events form
3. Click Search
4. Check the **Output** window in Visual Studio (View → Output)
5. Look for lines starting with "Search started:" and "Date filter:"

### Option 2: Check the Diagnostic Message
1. Run the app
2. Click Search
3. If no results, you'll see a message box showing:
   - Your search criteria
   - How many events are in the database

## Common Issues to Check

### Issue 1: No Events in Database
**Symptom:** Message shows "Total events in DB: 0"
**Solution:** Events didn't seed properly. Check `Data/events.xml` exists.

### Issue 2: Date Range Doesn't Include Events
**Symptom:** Debug shows "After date filter: 0 events"
**Solution:** Sample events are created as DateTime.Now + X days. Your date range might be in the past.

**Fix:** In LocalEventsForm, change InitializeForm():
```csharp
// Current (might not work if events are old)
dtpFromDate.Value = DateTime.Now;
dtpToDate.Value = DateTime.Now.AddDays(30);

// Try this instead to include all events
dtpFromDate.Value = DateTime.Now.AddDays(-30);  // Go back 30 days
dtpToDate.Value = DateTime.Now.AddDays(60);     // Go forward 60 days
```

### Issue 3: Index Not Built
**Symptom:** Exception in search
**Solution:** EventIndexes.BuildIndexes() should run on app start. Check EventStore constructor.

## Quick Test

To verify search is being called at all:

1. Open `LocalEventsForm.cs`
2. Find `btnSearch_Click`
3. Add at the very start:
```csharp
MessageBox.Show("Search button clicked!");
```
4. Run and click Search
5. If you see the message → button works, issue is in search logic
6. If you don't see it → button click not wired properly

## Rebuild Steps

If nothing helps:

1. **Close Visual Studio**
2. Delete `bin` and `obj` folders
3. Reopen solution
4. **Build → Rebuild Solution**
5. Run again

## Expected Behavior

When you click Search with default settings (no keyword, "All Categories", today to +30 days):
- Should show all events within date range
- Debug output should show:
  ```
  Search started: Total events=10
  Date filter: 2025-01-15 to 2025-02-14
  After date filter: X events (where X > 0)
  Search complete: Returning X events
  ```

## Let Me Know

After running with the debug version, please tell me:
1. What does the diagnostic message box show?
2. What do you see in the Output window (if running in debug mode)?
3. Are there any error messages?

This will help me pinpoint the exact issue!

