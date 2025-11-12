using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MunicipalServiceApp.Models;
using MunicipalServiceApp.Repositories;

namespace MunicipalServiceApp
{
    /// <summary>
    /// Form for viewing and managing service requests.
    /// Displays service requests in a list with filtering capabilities.
    /// </summary>
    public partial class ServiceRequestStatusForm : Form
    {
        private readonly IServiceRequestRepository _repository;

        public ServiceRequestStatusForm()
        {
            InitializeComponent();
            _repository = new InMemoryServiceRequestRepository();
            InitializeForm();
        }

        /// <summary>
        /// Sets up the form when it loads - populates status filter and loads requests.
        /// </summary>
        private void InitializeForm()
        {
            // Populate status filter dropdown
            PopulateStatusFilter();

            // Load all service requests into the list
            LoadAllRequests();

            // Set default priority filter to show all
            cmbPriorityFilter.SelectedIndex = 0;
        }

        /// <summary>
        /// Fills the status combobox with status options.
        /// </summary>
        private void PopulateStatusFilter()
        {
            cmbStatusFilter.Items.Clear();
            cmbStatusFilter.Items.Add("All Statuses");
            cmbStatusFilter.Items.Add("Submitted");
            cmbStatusFilter.Items.Add("In Progress");
            cmbStatusFilter.Items.Add("Completed");
            cmbStatusFilter.SelectedIndex = 0;
        }

        /// <summary>
        /// Loads all service requests into the ListView.
        /// </summary>
        private void LoadAllRequests()
        {
            var allRequests = _repository.GetAll().ToList();
            DisplayRequests(allRequests);
            UpdateStatusLabel($"Showing {allRequests.Count} service request(s)");
        }

        /// <summary>
        /// Displays a list of service requests in the ListView.
        /// </summary>
        private void DisplayRequests(List<ServiceRequest> requests)
        {
            lvRequests.Items.Clear();

            // Sort by priority (1 = high, 5 = low) and then by date submitted (newest first)
            var sortedRequests = requests
                .OrderBy(r => r.Priority)
                .ThenByDescending(r => r.DateSubmitted)
                .ToList();

            foreach (var request in sortedRequests)
            {
                var item = new ListViewItem(request.Title);
                item.SubItems.Add(request.RequestId);
                item.SubItems.Add(request.Status);
                item.SubItems.Add(request.Priority.ToString());
                item.SubItems.Add(request.DateSubmitted.ToString("dd/MM/yyyy HH:mm"));
                item.SubItems.Add(request.AssignedTeamId ?? "Unassigned");
                item.SubItems.Add($"({request.Latitude:F4}, {request.Longitude:F4})");
                item.SubItems.Add(request.Description);
                item.Tag = request; // Store the request object for later use

                // Color code by priority (1 = high priority = red tint, 5 = low priority = green tint)
                if (request.Priority == 1)
                    item.BackColor = System.Drawing.Color.LightPink;
                else if (request.Priority == 2)
                    item.BackColor = System.Drawing.Color.LightYellow;
                else if (request.Priority >= 4)
                    item.BackColor = System.Drawing.Color.LightGreen;

                lvRequests.Items.Add(item);
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
            try
            {
                var allRequests = _repository.GetAll().ToList();
                var filteredRequests = allRequests.AsEnumerable();

                // Filter by keyword
                string keyword = txtSearch.Text.Trim().ToLower();
                if (!string.IsNullOrEmpty(keyword))
                {
                    filteredRequests = filteredRequests.Where(r =>
                        r.Title.ToLower().Contains(keyword) ||
                        r.Description.ToLower().Contains(keyword) ||
                        r.RequestId.ToLower().Contains(keyword)
                    );
                }

                // Filter by status
                string statusFilter = cmbStatusFilter.SelectedItem?.ToString();
                if (statusFilter != null && statusFilter != "All Statuses")
                {
                    filteredRequests = filteredRequests.Where(r => r.Status == statusFilter);
                }

                // Filter by priority
                string priorityFilter = cmbPriorityFilter.SelectedItem?.ToString();
                if (priorityFilter != null && priorityFilter != "All Priorities")
                {
                    // Extract priority number from "Priority 1 (High)" or "Priority 2"
                    var parts = priorityFilter.Split(' ');
                    if (parts.Length >= 2 && int.TryParse(parts[1], out int priority))
                    {
                        filteredRequests = filteredRequests.Where(r => r.Priority == priority);
                    }
                }

                var results = filteredRequests.ToList();
                DisplayRequests(results);
                UpdateStatusLabel($"Found {results.Count} service request(s) matching your criteria");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while searching: {ex.Message}", "Search Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbStatusFilter.SelectedIndex = 0;
            cmbPriorityFilter.SelectedIndex = 0;
            LoadAllRequests();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _repository.Load(); // Reload from JSON file
            LoadAllRequests();
        }

        private void lvRequests_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvRequests.SelectedItems.Count > 0)
            {
                var selectedRequest = lvRequests.SelectedItems[0].Tag as ServiceRequest;
                if (selectedRequest != null)
                {
                    // Show details in the details textbox
                    txtDetails.Text = $"Request ID: {selectedRequest.RequestId}\r\n" +
                                     $"Title: {selectedRequest.Title}\r\n" +
                                     $"Status: {selectedRequest.Status}\r\n" +
                                     $"Priority: {selectedRequest.Priority} (1=High, 5=Low)\r\n" +
                                     $"Date Submitted: {selectedRequest.DateSubmitted:yyyy-MM-dd HH:mm:ss}\r\n" +
                                     $"Assigned Team: {selectedRequest.AssignedTeamId ?? "Unassigned"}\r\n" +
                                     $"Location: Latitude {selectedRequest.Latitude:F6}, Longitude {selectedRequest.Longitude:F6}\r\n" +
                                     $"\r\nDescription:\r\n{selectedRequest.Description}";
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _repository.Save(); // Save any changes before closing
            this.Close();
        }
    }
}

