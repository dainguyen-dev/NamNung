using Umbraco.Core.Models.PublishedContent;

namespace NamNung.Web.Factories.Models
{
    public class SimpleLinkWithImage
    {
        public IPublishedElement PublishedElement { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Target { get; set; }
        public Image Image { get; set; }
    }
}