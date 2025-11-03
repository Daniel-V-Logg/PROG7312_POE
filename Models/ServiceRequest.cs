using System;

namespace MunicipalServiceApp.Models
{
    /// <summary>
    /// Represents a service request submitted to the municipal service application.
    /// </summary>
    public class ServiceRequest
    {
        /// <summary>
        /// Unique identifier for the service request
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Title of the service request
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Detailed description of the service request
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Date and time when the request was submitted
        /// </summary>
        public DateTime DateSubmitted { get; set; }

        /// <summary>
        /// Priority level (1 = high, 5 = low)
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Current status of the request (Submitted, In Progress, Completed)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Latitude coordinate of the request location
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude coordinate of the request location
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// ID of the team assigned to handle this request
        /// </summary>
        public string AssignedTeamId { get; set; }

        /// <summary>
        /// Parameterless constructor for serialization
        /// </summary>
        public ServiceRequest()
        {
            RequestId = Guid.NewGuid().ToString();
            DateSubmitted = DateTime.Now;
            Status = "Submitted";
            Priority = 3; // Default to medium priority
        }

        /// <summary>
        /// Constructor with all properties
        /// </summary>
        public ServiceRequest(string title, string description, int priority, double latitude, double longitude, string assignedTeamId = null)
        {
            RequestId = Guid.NewGuid().ToString();
            Title = title;
            Description = description;
            DateSubmitted = DateTime.Now;
            Priority = priority;
            Status = "Submitted";
            Latitude = latitude;
            Longitude = longitude;
            AssignedTeamId = assignedTeamId;
        }

        public override string ToString()
        {
            return $"{Title} - {Status} (Priority: {Priority})";
        }
    }
}

