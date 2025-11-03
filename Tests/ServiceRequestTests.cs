using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MunicipalServiceApp.Models;
using MunicipalServiceApp.Repositories;

namespace MunicipalServiceApp.Tests
{
    /// <summary>
    /// Unit tests for ServiceRequest model and InMemoryServiceRequestRepository.
    /// </summary>
    public static class ServiceRequestTests
    {
        /// <summary>
        /// Runs all service request tests
        /// </summary>
        public static void RunAllTests()
        {
            Console.WriteLine("=========================================");
            Console.WriteLine("SERVICE REQUEST TESTS");
            Console.WriteLine("=========================================\n");

            TestCreateSampleRequests();
            TestRepositoryOperations();
            TestSaveAndLoad();

            Console.WriteLine("\n=========================================");
            Console.WriteLine("ALL SERVICE REQUEST TESTS COMPLETE");
            Console.WriteLine("=========================================");
        }

        /// <summary>
        /// Creates 10 sample service requests and verifies they are created correctly
        /// </summary>
        private static void TestCreateSampleRequests()
        {
            Console.WriteLine("TEST 1: Create 10 Sample Service Requests");
            Console.WriteLine("-------------------------------------------");

            var repository = new InMemoryServiceRequestRepository();
            var sampleRequests = CreateSampleRequests();

            foreach (var request in sampleRequests)
            {
                repository.Add(request);
                Console.WriteLine($"Created: {request}");
            }

            var allRequests = repository.GetAll().ToList();
            Console.WriteLine($"\nTotal requests created: {allRequests.Count}");
            Console.WriteLine($"Expected: 10");
            Console.WriteLine($"Status: {(allRequests.Count == 10 ? "PASS" : "FAIL")}");

            // Display all requests
            Console.WriteLine("\nAll Service Requests:");
            foreach (var request in allRequests)
            {
                Console.WriteLine($"  ID: {request.RequestId}");
                Console.WriteLine($"  Title: {request.Title}");
                Console.WriteLine($"  Priority: {request.Priority}");
                Console.WriteLine($"  Status: {request.Status}");
                Console.WriteLine($"  Location: ({request.Latitude}, {request.Longitude})");
                Console.WriteLine($"  Assigned Team: {request.AssignedTeamId ?? "None"}");
                Console.WriteLine($"  Submitted: {request.DateSubmitted:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Tests repository operations (GetById, Update, Delete)
        /// </summary>
        private static void TestRepositoryOperations()
        {
            Console.WriteLine("TEST 2: Repository Operations");
            Console.WriteLine("------------------------------");

            var repository = new InMemoryServiceRequestRepository();
            var sampleRequests = CreateSampleRequests();

            // Add all requests
            foreach (var request in sampleRequests)
            {
                repository.Add(request);
            }

            // Test GetById
            var firstRequest = sampleRequests.First();
            var retrieved = repository.GetById(firstRequest.RequestId);
            Console.WriteLine($"GetById Test: {(retrieved != null && retrieved.RequestId == firstRequest.RequestId ? "PASS" : "FAIL")}");

            // Test Update
            retrieved.Status = "In Progress";
            retrieved.Priority = 2;
            repository.Update(retrieved);
            var updated = repository.GetById(firstRequest.RequestId);
            Console.WriteLine($"Update Test: {(updated.Status == "In Progress" && updated.Priority == 2 ? "PASS" : "FAIL")}");

            // Test Delete
            var deleteResult = repository.Delete(firstRequest.RequestId);
            var deletedCheck = repository.GetById(firstRequest.RequestId);
            Console.WriteLine($"Delete Test: {(deleteResult && deletedCheck == null ? "PASS" : "FAIL")}");
            Console.WriteLine($"Requests remaining: {repository.GetAll().Count()}");

            Console.WriteLine();
        }

        /// <summary>
        /// Tests save and load functionality with JSON persistence
        /// </summary>
        private static void TestSaveAndLoad()
        {
            Console.WriteLine("TEST 3: Save and Load from JSON");
            Console.WriteLine("--------------------------------");

            // Create a temporary file path for testing
            var tempFilePath = Path.Combine(Path.GetTempPath(), $"serviceRequests_test_{Guid.NewGuid()}.json");

            try
            {
                var repository1 = new InMemoryServiceRequestRepository(tempFilePath);
                var sampleRequests = CreateSampleRequests();

                // Add all requests
                foreach (var request in sampleRequests)
                {
                    repository1.Add(request);
                }

                // Save to file
                repository1.Save();
                Console.WriteLine($"Save Test: {(File.Exists(tempFilePath) ? "PASS" : "FAIL")}");

                // Create a new repository instance and load from file
                var repository2 = new InMemoryServiceRequestRepository(tempFilePath);
                var loadedRequests = repository2.GetAll().ToList();

                Console.WriteLine($"Load Test: {(loadedRequests.Count == 10 ? "PASS" : "FAIL")}");
                Console.WriteLine($"Loaded {loadedRequests.Count} requests from JSON file");

                // Verify data integrity
                bool dataMatches = true;
                foreach (var original in sampleRequests)
                {
                    var loaded = loadedRequests.FirstOrDefault(r => r.RequestId == original.RequestId);
                    if (loaded == null || 
                        loaded.Title != original.Title || 
                        loaded.Description != original.Description ||
                        loaded.Priority != original.Priority)
                    {
                        dataMatches = false;
                        break;
                    }
                }

                Console.WriteLine($"Data Integrity Test: {(dataMatches ? "PASS" : "FAIL")}");
            }
            finally
            {
                // Clean up temporary file
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Creates 10 sample service requests with various properties
        /// </summary>
        private static List<ServiceRequest> CreateSampleRequests()
        {
            return new List<ServiceRequest>
            {
                new ServiceRequest
                {
                    RequestId = "SR-001",
                    Title = "Pothole Repair on Main Street",
                    Description = "Large pothole on Main Street near intersection with Oak Avenue causing vehicle damage.",
                    DateSubmitted = DateTime.Now.AddDays(-5),
                    Priority = 1,
                    Status = "Submitted",
                    Latitude = -26.2041,
                    Longitude = 28.0473,
                    AssignedTeamId = "Road-Maintenance-01"
                },
                new ServiceRequest
                {
                    RequestId = "SR-002",
                    Title = "Broken Street Light on Elm Drive",
                    Description = "Street light #42 on Elm Drive is not working. Area is dark at night, safety concern.",
                    DateSubmitted = DateTime.Now.AddDays(-3),
                    Priority = 2,
                    Status = "In Progress",
                    Latitude = -26.2141,
                    Longitude = 28.0573,
                    AssignedTeamId = "Electrical-Team-02"
                },
                new ServiceRequest
                {
                    RequestId = "SR-003",
                    Title = "Garbage Collection Missed",
                    Description = "Garbage collection was missed on Tuesday for 123 Park Avenue. Bins still full.",
                    DateSubmitted = DateTime.Now.AddDays(-1),
                    Priority = 3,
                    Status = "Submitted",
                    Latitude = -26.2241,
                    Longitude = 28.0673,
                    AssignedTeamId = null
                },
                new ServiceRequest
                {
                    RequestId = "SR-004",
                    Title = "Water Leak at Community Center",
                    Description = "Water leak detected in the men's restroom at the community center. Water pooling on floor.",
                    DateSubmitted = DateTime.Now.AddDays(-7),
                    Priority = 1,
                    Status = "Completed",
                    Latitude = -26.2341,
                    Longitude = 28.0773,
                    AssignedTeamId = "Plumbing-Team-01"
                },
                new ServiceRequest
                {
                    RequestId = "SR-005",
                    Title = "Overgrown Tree Branches",
                    Description = "Tree branches hanging over sidewalk on Pine Street blocking pedestrian access.",
                    DateSubmitted = DateTime.Now.AddDays(-2),
                    Priority = 4,
                    Status = "Submitted",
                    Latitude = -26.2441,
                    Longitude = 28.0873,
                    AssignedTeamId = null
                },
                new ServiceRequest
                {
                    RequestId = "SR-006",
                    Title = "Damaged Sidewalk Tiles",
                    Description = "Several sidewalk tiles are cracked and uneven near the library entrance, tripping hazard.",
                    DateSubmitted = DateTime.Now.AddDays(-4),
                    Priority = 2,
                    Status = "In Progress",
                    Latitude = -26.2541,
                    Longitude = 28.0973,
                    AssignedTeamId = "Infrastructure-Team-01"
                },
                new ServiceRequest
                {
                    RequestId = "SR-007",
                    Title = "Noise Complaint - Construction After Hours",
                    Description = "Construction work continuing after 10 PM on Maple Avenue. Excessive noise disturbing residents.",
                    DateSubmitted = DateTime.Now.AddHours(-6),
                    Priority = 2,
                    Status = "Submitted",
                    Latitude = -26.2641,
                    Longitude = 28.1073,
                    AssignedTeamId = null
                },
                new ServiceRequest
                {
                    RequestId = "SR-008",
                    Title = "Parks Playground Equipment Needs Repair",
                    Description = "Swings are broken and slide has sharp edges at Central Park playground. Safety concern for children.",
                    DateSubmitted = DateTime.Now.AddDays(-6),
                    Priority = 2,
                    Status = "In Progress",
                    Latitude = -26.2741,
                    Longitude = 28.1173,
                    AssignedTeamId = "Parks-Maintenance-01"
                },
                new ServiceRequest
                {
                    RequestId = "SR-009",
                    Title = "Traffic Sign Vandalized",
                    Description = "Stop sign at corner of First and Second Street has been vandalized and is no longer visible.",
                    DateSubmitted = DateTime.Now.AddDays(-2),
                    Priority = 1,
                    Status = "Completed",
                    Latitude = -26.2841,
                    Longitude = 28.1273,
                    AssignedTeamId = "Traffic-Control-01"
                },
                new ServiceRequest
                {
                    RequestId = "SR-010",
                    Title = "Request for New Benches in Park",
                    Description = "Residents requesting additional benches to be installed along the walking path in Riverside Park.",
                    DateSubmitted = DateTime.Now,
                    Priority = 5,
                    Status = "Submitted",
                    Latitude = -26.2941,
                    Longitude = 28.1373,
                    AssignedTeamId = null
                }
            };
        }
    }
}

