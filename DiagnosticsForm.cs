using System;
using System.Linq;
using System.Windows.Forms;
using MunicipalServiceApp.Data;

namespace MunicipalServiceApp
{
    /// <summary>
    /// Diagnostics form to inspect data structures and verify system performance.
    /// Shows sizes of all indexes and top events by popularity.
    /// </summary>
    public partial class DiagnosticsForm : Form
    {
        public DiagnosticsForm()
        {
            InitializeComponent();
            LoadDiagnostics();
        }

        private void LoadDiagnostics()
        {
            // Get index statistics
            txtIndexStats.Text = EventIndexes.GetIndexStats();

            // Show top events by popularity
            var topEvents = EventIndexes.PopularEvents.GetTopN(5);
            txtTopEvents.Text = "Top 5 Most Popular Events:\r\n\r\n";

            for (int i = 0; i < topEvents.Count; i++)
            {
                var evt = topEvents[i];
                txtTopEvents.Text += $"{i + 1}. {evt.Title}\r\n";
                txtTopEvents.Text += $"   Popularity Score: {evt.PopularityScore}\r\n";
                txtTopEvents.Text += $"   Date: {evt.Date:dd/MM/yyyy}\r\n";
                txtTopEvents.Text += $"   Category: {evt.Category}\r\n\r\n";
            }

            // Show category distribution
            txtCategoryDist.Text = "Events by Category:\r\n\r\n";
            foreach (var category in EventIndexes.UniqueCategories.OrderBy(c => c))
            {
                var count = EventIndexes.EventsByCategory[category].Count;
                txtCategoryDist.Text += $"{category}: {count} event(s)\r\n";
            }

            // Show recent searches
            txtRecentSearches.Text = "Recent Search Queries:\r\n\r\n";
            var searches = EventIndexes.GetRecentSearches(10);
            if (searches.Count == 0)
            {
                txtRecentSearches.Text += "No searches yet.\r\n";
            }
            else
            {
                foreach (var search in searches)
                {
                    txtRecentSearches.Text += $"- {search}\r\n";
                }
            }

            // Show date range coverage
            if (EventIndexes.EventsByDate.Count > 0)
            {
                var firstDate = EventIndexes.EventsByDate.Keys.Min();
                var lastDate = EventIndexes.EventsByDate.Keys.Max();
                txtDateRange.Text = $"Date Range Coverage:\r\n\r\n";
                txtDateRange.Text += $"Earliest Event: {firstDate:dd/MM/yyyy}\r\n";
                txtDateRange.Text += $"Latest Event: {lastDate:dd/MM/yyyy}\r\n";
                txtDateRange.Text += $"Total Date Entries: {EventIndexes.EventsByDate.Count}\r\n\r\n";
                txtDateRange.Text += "Events per Date:\r\n";
                foreach (var kvp in EventIndexes.EventsByDate.OrderBy(x => x.Key))
                {
                    txtDateRange.Text += $"{kvp.Key:dd/MM/yyyy}: {kvp.Value.Count} event(s)\r\n";
                }
            }

            // Persistence status
            txtPersistence.Text = "Data Persistence Status:\r\n\r\n";
            txtPersistence.Text += $"Total Events Loaded: {EventStore.Events.Count}\r\n";
            txtPersistence.Text += $"Total Search History: {EventStore.SearchHistory.Count}\r\n";
            txtPersistence.Text += $"Events File: Data\\events.xml\r\n";
            txtPersistence.Text += $"Search History File: Data\\searchHistory.xml\r\n";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDiagnostics();
            MessageBox.Show("Diagnostics refreshed!", "Refresh", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

