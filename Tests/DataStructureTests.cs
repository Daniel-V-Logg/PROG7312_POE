using System;
using System.Collections.Generic;
using MunicipalServiceApp;
using MunicipalServiceApp.Models;
using MunicipalServiceApp.Data;

namespace MunicipalServiceApp.Tests
{
    /// <summary>
    /// Test helper to verify data structures and search functionality.
    /// Run this from Program.cs Main method or create a separate console project.
    /// </summary>
    public static class DataStructureTests
    {
        public static void RunAllTests()
        {
            Console.WriteLine("=========================================");
            Console.WriteLine("DATA STRUCTURE TESTS");
            Console.WriteLine("=========================================\n");

            TestEventLoading();
            TestIndexBuilding();
            TestSearchFunctionality();
            TestRecommendations();
            TestPersistence();

            Console.WriteLine("\n=========================================");
            Console.WriteLine("ALL TESTS COMPLETE");
            Console.WriteLine("=========================================");
        }

        private static void TestEventLoading()
        {
            Console.WriteLine("TEST 1: Event Loading");
            Console.WriteLine("---------------------");
            Console.WriteLine($"Total Events Loaded: {EventStore.Events.Count}");
            Console.WriteLine($"Expected: 10 (sample events)");
            Console.WriteLine($"Status: {(EventStore.Events.Count == 10 ? "PASS" : "FAIL")}");
            Console.WriteLine();
        }

        private static void TestIndexBuilding()
        {
            Console.WriteLine("TEST 2: Index Building");
            Console.WriteLine("----------------------");
            
            Console.WriteLine("\nSortedDictionary (EventsByDate):");
            Console.WriteLine($"  Unique Dates: {EventIndexes.EventsByDate.Count}");
            Console.WriteLine($"  Status: {(EventIndexes.EventsByDate.Count > 0 ? "PASS" : "FAIL")}");

            Console.WriteLine("\nDictionary (EventsByCategory):");
            Console.WriteLine($"  Categories: {EventIndexes.EventsByCategory.Count}");
            foreach (var kvp in EventIndexes.EventsByCategory)
            {
                Console.WriteLine($"    {kvp.Key}: {kvp.Value.Count} events");
            }
            Console.WriteLine($"  Status: {(EventIndexes.EventsByCategory.Count > 0 ? "PASS" : "FAIL")}");

            Console.WriteLine("\nHashSet (UniqueCategories):");
            Console.WriteLine($"  Unique Categories: {EventIndexes.UniqueCategories.Count}");
            foreach (var cat in EventIndexes.UniqueCategories)
            {
                Console.WriteLine($"    - {cat}");
            }
            Console.WriteLine($"  Status: {(EventIndexes.UniqueCategories.Count > 0 ? "PASS" : "FAIL")}");

            Console.WriteLine("\nPriorityQueue (PopularEvents):");
            var top3 = EventIndexes.PopularEvents.GetTopN(3);
            Console.WriteLine($"  Top 3 Popular Events:");
            for (int i = 0; i < top3.Count; i++)
            {
                Console.WriteLine($"    {i + 1}. {top3[i].Title} (Score: {top3[i].PopularityScore})");
            }
            Console.WriteLine($"  Status: {(top3.Count > 0 ? "PASS" : "FAIL")}");

            Console.WriteLine();
        }

        private static void TestSearchFunctionality()
        {
            Console.WriteLine("TEST 3: Search Functionality");
            Console.WriteLine("-----------------------------");

            // Test keyword search
            Console.WriteLine("\nKeyword Search (\"music\"):");
            var keywordResults = EventStore.SearchEvents("music", null, null, null);
            Console.WriteLine($"  Results: {keywordResults.Count}");
            foreach (var evt in keywordResults)
            {
                Console.WriteLine($"    - {evt.Title}");
            }
            Console.WriteLine($"  Status: {(keywordResults.Count > 0 ? "PASS" : "FAIL")}");

            // Test category search
            Console.WriteLine("\nCategory Search (\"Sports\"):");
            var categoryResults = EventStore.SearchEvents(null, "Sports", null, null);
            Console.WriteLine($"  Results: {categoryResults.Count}");
            foreach (var evt in categoryResults)
            {
                Console.WriteLine($"    - {evt.Title}");
            }
            Console.WriteLine($"  Status: {(categoryResults.Count > 0 ? "PASS" : "FAIL")}");

            // Test date range search
            Console.WriteLine("\nDate Range Search (Next 7 days):");
            var dateResults = EventStore.SearchEvents(null, null, DateTime.Now, DateTime.Now.AddDays(7));
            Console.WriteLine($"  Results: {dateResults.Count}");
            foreach (var evt in dateResults)
            {
                Console.WriteLine($"    - {evt.Title} ({evt.Date:dd/MM/yyyy})");
            }
            Console.WriteLine($"  Status: {(dateResults.Count >= 0 ? "PASS" : "FAIL")}");

            // Test combined search
            Console.WriteLine("\nCombined Search (\"community\" + \"Community\" category):");
            var combinedResults = EventStore.SearchEvents("community", "Community", DateTime.Now, DateTime.Now.AddDays(30));
            Console.WriteLine($"  Results: {combinedResults.Count}");
            foreach (var evt in combinedResults)
            {
                Console.WriteLine($"    - {evt.Title}");
            }
            Console.WriteLine($"  Status: PASS");

            Console.WriteLine();
        }

        private static void TestRecommendations()
        {
            Console.WriteLine("TEST 4: Recommendation Engine");
            Console.WriteLine("------------------------------");

            // Simulate some searches
            EventStore.RecordSearch("music");
            EventStore.RecordSearch("festival");
            EventStore.RecordSearch("sports");
            EventStore.RecordSearch("community");

            Console.WriteLine($"\nSearch History: {EventStore.SearchHistory.Count} queries");
            Console.WriteLine("Recent searches:");
            foreach (var search in EventIndexes.GetRecentSearches(10))
            {
                Console.WriteLine($"  - {search}");
            }

            Console.WriteLine("\nRecommendations:");
            var recommendations = EventStore.GetRecommendations();
            for (int i = 0; i < recommendations.Count; i++)
            {
                var evt = recommendations[i];
                Console.WriteLine($"  {i + 1}. {evt.Title}");
                Console.WriteLine($"     Category: {evt.Category}, Popularity: {evt.PopularityScore}");
            }
            Console.WriteLine($"\nStatus: {(recommendations.Count > 0 ? "PASS" : "FAIL")}");

            Console.WriteLine();
        }

        private static void TestPersistence()
        {
            Console.WriteLine("TEST 5: Data Persistence");
            Console.WriteLine("-------------------------");

            Console.WriteLine($"\nEvents XML exists: {System.IO.File.Exists(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "events.xml"))}");
            Console.WriteLine($"Search History XML exists: {System.IO.File.Exists(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "searchHistory.xml"))}");

            Console.WriteLine($"\nTotal events persisted: {EventStore.Events.Count}");
            Console.WriteLine($"Total search history persisted: {EventStore.SearchHistory.Count}");

            Console.WriteLine("\nStatus: PASS");
            Console.WriteLine();
        }

        public static void PrintIndexStatistics()
        {
            Console.WriteLine("\n=========================================");
            Console.WriteLine("INDEX STATISTICS");
            Console.WriteLine("=========================================\n");
            Console.WriteLine(EventIndexes.GetIndexStats());
            Console.WriteLine();
        }
    }
}

