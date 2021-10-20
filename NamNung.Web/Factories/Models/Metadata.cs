using System.Collections.Generic;

namespace NamNung.Web.Factories.Models
{
    public class Metadata
    {
        public string BrowserTitle { get; set; }
        public string IconUrl { get; set; }
        public string Title { get; set; }
        public string Descripton { get; set; }
        public string Keywords { get; set; }
        public string Author { get; set; }
        public string FullUrl { get; set; }
        public IEnumerable<string> Images {get;set;}
    }
}