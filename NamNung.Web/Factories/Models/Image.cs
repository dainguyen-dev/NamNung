using Umbraco.Core.Models.PublishedContent;

namespace NamNung.Web.Factories.Models
{
    public class Image
    {
        public IPublishedContent PublishedContent { get; set; }
        public string Url { get; set; }
        public string Alt { get; set; }
    }
}