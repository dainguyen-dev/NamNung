using System.Collections.Generic;

namespace NamNung.Web.Models
{
    public class CustomContentDashBoard
    {
        public IEnumerable<PageDetailApi> Contents { get; set; }
        public IEnumerable<string> Status { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }

    public class PageDetailApi
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string UpdateDate { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
    }
}