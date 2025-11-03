using System.Collections.Generic;
using MunicipalServiceApp.Models;

namespace MunicipalServiceApp.Repositories
{
    /// <summary>
    /// Repository interface for managing service requests.
    /// </summary>
    public interface IServiceRequestRepository
    {
        /// <summary>
        /// Gets all service requests
        /// </summary>
        IEnumerable<ServiceRequest> GetAll();

        /// <summary>
        /// Gets a service request by its ID
        /// </summary>
        ServiceRequest GetById(string requestId);

        /// <summary>
        /// Adds a new service request
        /// </summary>
        void Add(ServiceRequest request);

        /// <summary>
        /// Updates an existing service request
        /// </summary>
        void Update(ServiceRequest request);

        /// <summary>
        /// Deletes a service request by ID
        /// </summary>
        bool Delete(string requestId);

        /// <summary>
        /// Saves all requests to persistent storage (JSON file)
        /// </summary>
        void Save();

        /// <summary>
        /// Loads all requests from persistent storage (JSON file)
        /// </summary>
        void Load();
    }
}

