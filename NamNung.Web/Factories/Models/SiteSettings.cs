using Umbraco.Core.Models.PublishedContent;

namespace NamNung.Web.Factories.Models
{
    public class SiteSettings
    {
        public IPublishedContent PublishedContent { get; set; }
        public string LogoUrl { get; set; }
        public string LogoAlt { get; set; }
        public string ContactTitle { get; set; }
        public string ContactAddress { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactEmail { get; set; }
        public string ContactFaxNumber { get; set; }
        public string Copyright { get; set; }
    }
}