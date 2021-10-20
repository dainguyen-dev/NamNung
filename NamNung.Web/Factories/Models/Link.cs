using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace NamNung.Web.Factories.Models
{
    public class Link
    {
        public IPublishedContent PublishedContent { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Target { get; set; }
        public List<Link> ChildLinks { get; set; }
    }
}