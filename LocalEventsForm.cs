using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MunicipalServiceApp.Models;

namespace MunicipalServiceApp
{
    /// <summary>
    /// Main form for browsing local events and announcements.
    /// Provides search, filtering, and recommendation features.
    /// </summary>
    public partial class LocalEventsForm : Form
    {
        public LocalEventsForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        /// <summary>
        /// Sets up the form when it loads - populates category dropdown and loads events.
        /// </summary>
        private void InitializeForm()
        {
            // Populate category filter dropdown
            PopulateCategoryFilter();

            // Load all events into the list
            LoadAllEvents();

            // Set default date range to show next 30 days
            dtpFromDate.Value = DateTime.Now;
            dtpToDate.Value = DateTime.Now.AddDays(30);
        }

        /// <summary>
        /// Fills the category combobox with unique categories from events.
        /// </summary>
        private void PopulateCategoryFilter()
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All Categories"); // Default option

            // Get unique categories from all events
            var categories = EventStore.Events
                .Select(e => e.Category)
                .Distinct()
                .OrderBy(c => c);

            foreach (var category in categories)
            {
                cmbCategory.Items.Add(category);
            }

            cmbCategory.SelectedIndex = 0; // Select "All Categories"
        }

        /// <summary>
        /// Loads all events into the ListView.
        /// Called on form load and after clearing filters.
        /// </summary>
        private void LoadAllEvents()
        {
            DisplayEvents(EventStore.Events);
            UpdateStatusLabel($"Showing {EventStore.Events.Count} events");
        }

        /// <summary>
        /// Displays a list of events in the ListView.
        /// Clears existing items first.
        /// </summary>
        private void DisplayEvents(List<Event> events)
        {
            lvEvents.Items.Clear();

            // Sort events by date (soonest first)
            var sortedEvents = events.OrderBy(e => e.Date).ToList();

            foreach (var evt in sortedEvents)
            {
                var item = new ListViewItem(evt.Title);
                item.SubItems.Add(evt.Date.ToString("dd/MM/yyyy"));
                item.SubItems.Add(evt.Category);
                item.SubItems.Add(evt.Location);
                item.SubItems.Add(evt.Description);
                item.Tag = evt; // Store the event object for later use

                lvEvents.Items.Add(item);
            }
        }

        /// <summary>
        /// Updates the status label with current operation info.
        /// </summary>
        private void UpdateStatusLabel(string message)
        {
            lblStatus.Text = message;
        }

        // --- Button Click Handlers ---

        private void btnSearch_Click(object sender, EventArgs e)
        {
            UpdateStatusLabel("Searching...");

            try
            {
                // Get search parameters
                string keyword = txtSearch.Text.Trim();
                string category = cmbCategory.SelectedItem?.ToString();
                DateTime fromDate = dtpFromDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date;

                // Validate date range
                if (toDate < fromDate)
                {
                    MessageBox.Show("'To Date' must be after 'From Date'", "Invalid Date Range",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Debug: Show search parameters
                string debugMsg = $"Search Parameters:\n" +
                                 $"Keyword: '{keyword}'\n" +
                                 $"Category: '{category}'\n" +
                                 $"From: {fromDate:yyyy-MM-dd}\n" +
                                 $"To: {toDate:yyyy-MM-dd}\n" +
                                 $"Total Events: {EventStore.Events.Count}";
                System.Diagnostics.Debug.WriteLine(debugMsg);

                // Perform search using indexes
                var results = EventStore.SearchEvents(keyword, category, fromDate, toDate);

                System.Diagnostics.Debug.WriteLine($"Results found: {results.Count}");

                // Display results
                DisplayEvents(results);

                // Update status
                if (results.Count == 0)
                {
                    UpdateStatusLabel("No events found matching your search criteria");
                    
                    // Temporary diagnostic
                    MessageBox.Show($"No results found.\n\n" +
                                   $"Search used:\n" +
                                   $"Keyword: '{keyword}'\n" +
                                   $"Category: '{category}'\n" +
                                   $"Date Range: {fromDate:MM/dd/yyyy} to {toDate:MM/dd/yyyy}\n" +
                                   $"Total events in DB: {EventStore.Events.Count}\n\n" +
                                   $"Check the Output window in Visual Studio for debug info.",
                                   "Debug Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    UpdateStatusLabel($"Found {results.Count} event(s) matching your search");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Search error: {ex.Message}\n\nStack: {ex.StackTrace}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatusLabel("Search failed");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Reset all filters
            txtSearch.Clear();
            cmbCategory.SelectedIndex = 0;
            dtpFromDate.Value = DateTime.Now;
            dtpToDate.Value = DateTime.Now.AddDays(30);

            // Reload all events
            LoadAllEvents();
            UpdateStatusLabel("Filters cleared");
        }

        private void btnRecommend_Click(object sender, EventArgs e)
        {
            UpdateStatusLabel("Generating recommendations...");

            try
            {
                // Get recommendations based on search history
                var recommendations = EventStore.GetRecommendations();

                if (recommendations.Count == 0)
                {
                    MessageBox.Show("No recommendations available yet.\n\nTry searching for events first to build your preferences!",
                        "Recommendations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Build recommendation message
                var message = "Based on your search history, we recommend:\n\n";
                for (int i = 0; i < recommendations.Count; i++)
                {
                    var evt = recommendations[i];
                    message += $"{i + 1}. {evt.Title}\n";
                    message += $"   {evt.Date:dd/MM/yyyy} - {evt.Category}\n";
                    message += $"   {evt.Location}\n\n";
                }

                message += "Click an event in the list below to see more details!";

                // Display recommendations in message box
                MessageBox.Show(message, "Recommended Events For You", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Also display recommended events in the ListView
                DisplayEvents(recommendations);
                UpdateStatusLabel($"Showing {recommendations.Count} recommended events based on your interests");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Recommendation error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatusLabel("Recommendation failed");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Event handler for when user selects an event in the list.
        /// Increments popularity score for recommendation tracking.
        /// </summary>
        private void lvEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvEvents.SelectedItems.Count > 0)
            {
                var selectedEvent = lvEvents.SelectedItems[0].Tag as Event;
                if (selectedEvent != null)
                {
                    // Increment popularity when user views/selects an event
                    selectedEvent.IncrementPopularity();
                    EventStore.SaveEvents(); // Persist the popularity change
                }
            }
        }
    }
}

