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

        // --- Button Click Handlers (stub implementations for now) ---

        private void btnSearch_Click(object sender, EventArgs e)
        {
            UpdateStatusLabel("Searching...");

            // TODO: Implement actual search logic in next stage
            // For now, just show all events
            LoadAllEvents();
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

            // TODO: Implement recommendation logic in next stage
            MessageBox.Show("Recommendation feature coming in next stage!",
                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

