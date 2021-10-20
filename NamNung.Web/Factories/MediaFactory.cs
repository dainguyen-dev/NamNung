using NamNung.Web.Extensions;
using NamNung.Web.Factories.Models;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace NamNung.Web.Factories
{
    public static class MediaFactory
    {
        public static IEnumerable<BannerWithRichText> GetBannersWitRichText(IPublishedContent publishedContent, string alias)
        {
            var bannerItems = publishedContent.Value<IEnumerable<IPublishedElement>>(alias);

            if (bannerItems is null || !bannerItems.Any())
            {
                return Enumerable.Empty<BannerWithRichText>();
            }

            var banners = new List<BannerWithRichText>();
            foreach (var bannerItem in bannerItems)
            {
                var banner = new BannerWithRichText()
                {
                    PublishedElement = bannerItem,
                    Title = bannerItem.Value<string>(Constants.Fields.Title),
                    Body = bannerItem.Value<string>(Constants.Fields.Body),
                    LinkTitle = bannerItem.Value<string>(Constants.Fields.LinkTitle),
                    Images = bannerItem.GetMediaPicker(Constants.Fields.Images).Select(image => new Image
                    {
                        PublishedContent = image,
                        Url = image?.Url(),
                        Alt = image.Name
                    })
                };

                var link = bannerItem.Value<IEnumerable<Umbraco.Web.Models.Link>>(Constants.Fields.Links)?.FirstOrDefault();
                banner.LinkTarget = link?.Target;
                banner.LinkUrl = link?.Url;

                banners.Add(banner);
            }

            return banners;
        }

        public static IEnumerable<Banner> GetBanners(IPublishedContent publishedContent, string alias)
        {
            var bannerItems = publishedContent.Value<IEnumerable<IPublishedElement>>(alias);
            if (bannerItems is null || !bannerItems.Any())
            {
                return Enumerable.Empty<Banner>();
            }

            var banners = new List<Banner>();
            foreach (var bannerItem in bannerItems)
            {
                var banner = new Banner()
                {
                    PublishedElement = bannerItem,
                    Title = bannerItem.Value<string>(Constants.Fields.Title),
                    SubTitle = bannerItem.Value<string>(Constants.Fields.SubTitle),
                    LinkTitle = bannerItem.Value<string>(Constants.Fields.LinkTitle)
                };

                var link = bannerItem.Value<IEnumerable<Umbraco.Web.Models.Link>>(Constants.Fields.Links)?.FirstOrDefault();
                banner.LinkTarget = link?.Target;
                banner.LinkUrl = link?.Url;

                var image = bannerItem.GetSingleMediaPicker(Constants.Fields.Image);
                banner.ImageAlt = image?.Name;
                banner.ImageUrl = image?.Url();

                banners.Add(banner);
            }

            return banners;
        }
    }
}