using NamNung.Web.Factories.Models;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace NamNung.Web.Factories
{
    public static class SeoFactory
    {
        public static Metadata GetMetadata(IPublishedContent publishedContent, IPublishedContent root)
        {
            var pageContentDetail = PageContentFactory.GetPageContentDetail(publishedContent);
            var siteSettings = SiteSettingsFactory.GetSiteSettings(root);

            var metadata = new Metadata
            {
                IconUrl = siteSettings.LogoUrl,
                BrowserTitle = pageContentDetail.PublishedContent.Name,
                Author = string.Empty,
                Descripton = pageContentDetail.Description,
                Keywords = string.Join(",", pageContentDetail.Tags),
                Title = pageContentDetail.Title,
                FullUrl = pageContentDetail.PublishedContent.Url(null, UrlMode.Absolute),
                Images = pageContentDetail.Images.Select(image => image.PublishedContent.Url(null, UrlMode.Absolute))
            };

            return metadata;
        }
    }
}