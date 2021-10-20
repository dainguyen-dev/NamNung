using Umbraco.Core.Models.PublishedContent;

namespace NamNung.Web.Factories.Models
{
    public class Banner
    {
        public IPublishedElement PublishedElement { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string LinkTitle { get; set; }
        public string LinkUrl { get; set; }
        public string LinkTarget { get; set; }
        public string ImageUrl { get; set; }
        public string ImageAlt { get; set; }
    }
}