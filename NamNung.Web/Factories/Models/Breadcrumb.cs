using Umbraco.Core.Models.PublishedContent;

namespace NamNung.Web.Factories.Models
{
    public class Breadcrumb
    {
        public IPublishedContent PublishedContent { get; set; }
        public string Title { get; set; }
        public bool Active { get; set; }
    }
}