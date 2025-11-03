using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace MunicipalServiceApp
{
    public static class DataStore
    {
        private static readonly string DataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        private static readonly string FilePath = Path.Combine(DataFolder, "issues.xml");


        public static List<Issue> Issues { get; private set; } = new List<Issue>();


        static DataStore()
        {
            if (!Directory.Exists(DataFolder))
                Directory.CreateDirectory(DataFolder);


            Load();
        }


        public static void AddIssue(Issue issue)
        {
            Issues.Add(issue);
            Save();
        }


        public static void Save()
        {
            var serializer = new XmlSerializer(typeof(List<Issue>));
            using (var writer = new StreamWriter(FilePath))
            {
                serializer.Serialize(writer, Issues);
            }
        }


        public static void Load()
        {
            if (File.Exists(FilePath))
            {
                var serializer = new XmlSerializer(typeof(List<Issue>));
                using (var reader = new StreamReader(FilePath))
                {
                    Issues = (List<Issue>)serializer.Deserialize(reader);
                }
            }
        }
    }
}
