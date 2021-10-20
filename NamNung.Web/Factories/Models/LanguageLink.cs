using Umbraco.Core.Models.PublishedContent;

namespace NamNung.Web.Factories.Models
{
    public class LanguageLink
    {
        public IPublishedContent PublishedContent { get; set; }
        public string Url { get; set; }
        public string FullUrl { get; set; }
        public string CultureKey { get; set; }
    }
}