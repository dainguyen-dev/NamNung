using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace NamNung.Web.Factories.Models
{
    public class BannerWithRichText
    {
        public IPublishedElement PublishedElement { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string LinkTitle { get; set; }
        public string LinkUrl { get; set; }
        public string LinkTarget { get; set; }
        public IEnumerable<Image> Images { get; set; }
    }
}