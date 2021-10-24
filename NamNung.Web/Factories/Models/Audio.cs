using Umbraco.Core.Models.PublishedContent;

namespace NamNung.Web.Factories.Models
{
    public class Audio
    {
        public IPublishedContent PublishedContent { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
    }
}