using Umbraco.Core.Models.PublishedContent;

namespace NamNung.Web.Factories.Models
{
    public class SimpleLink
    {
        public IPublishedElement PublishedElement { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Target { get; set; }
    }
}