using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServiceApp
{
    public class Issue
    {
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string AttachmentPath { get; set; }
        public DateTime ReportedAt { get; set; }

        public Issue()
        {
        }

        public Issue(string location, string category, string description, string attachmentPath)
        {
            Location = location;
            Category = category;
            Description = description;
            AttachmentPath = attachmentPath;
            ReportedAt = DateTime.Now;
        }
    }
}
