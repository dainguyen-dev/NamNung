using System;
using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace NamNung.Web.Factories.Models
{
    public class PageContentDetail
    {
        public PageContentDetail()
        {
            Partner = new PageContentPartnerDetail();
        }

        public IPublishedContent PublishedContent { get; set; }
        public string HeadingTitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public Audio AudioBody { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public Image Image { get; set; }
        public bool Highlight { get; set; }
        public bool ShowOnHomePage { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<IPublishedContent> Category { get; set; }

        public PageContentPartnerDetail Partner { get; set; }
    }
}