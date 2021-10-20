using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using System.Linq;
using System.Collections.Generic;
using NamNung.Web.Factories.Models;
using NamNung.Web.Extensions;

namespace NamNung.Web.Factories
{
    public static class NavigationFactory
    {
        public static IEnumerable<Breadcrumb> GetBreadcrumbs(IPublishedContent publishContent)
        {
            var breadcrumbs = new List<Breadcrumb>();
            var breadcrumbItems = publishContent.AncestorsOrSelf().Reverse();
            foreach (var breadcrumbItem in breadcrumbItems)
            {
                breadcrumbs.Add(new Breadcrumb
                {
                    PublishedContent = breadcrumbItem,
                    Title = breadcrumbItem.Name,
                    Active = publishContent.Id.Equals(breadcrumbItem.Id)
                });
            }

            return breadcrumbs;
        }

        public static IEnumerable<SimpleLink> GetFooterMenu(IPublishedContent publishedContentAtRoot)
        {
            return GetSimpleMenuFromAtRoot(publishedContentAtRoot, Constants.Fields.FooterMenu);
        }

        public static IEnumerable<Link> GetHeaderMenu(IPublishedContent publishedContentAtRoot)
        {
            var headerMenus = new List<Link>();
            var headerMenuItems = publishedContentAtRoot.Value<IEnumerable<IPublishedElement>>(Constants.Fields.HeaderMenu);

            foreach (var headerMenuItem in headerMenuItems)
            {
                var link = headerMenuItem.Value<IEnumerable<Umbraco.Web.Models.Link>>(Constants.Fields.Links)?.FirstOrDefault();
                var childItems = headerMenuItem.Value<IEnumerable<IPublishedElement>>(Constants.Fields.ChildLinks);

                var headerMenu = new Link
                {
                    Title = headerMenuItem.GetProperty(Constants.Fields.Title)?.GetValue()?.ToString(),
                    Url = link?.Url,
                    Target = link?.Target,
                    ChildLinks = ComputeChildLinks(childItems)
                };

                headerMenus.Add(headerMenu);
            }

            return headerMenus;
        }

        public static IEnumerable<LanguageLink> GetLanguageMenu(IPublishedContent publishedContent)
        {
            var languageMenus = new List<LanguageLink>();

            foreach (var culture in publishedContent.Cultures)
            {
                var languageMenu = new LanguageLink
                {
                    PublishedContent = publishedContent,
                    CultureKey = culture.Key,
                    Url = publishedContent.Url(culture.Key),
                    FullUrl = publishedContent.Url(culture.Key, UrlMode.Absolute)
                };

                languageMenus.Add(languageMenu);
            }

            return languageMenus;
        }

        public static IEnumerable<SimpleLinkWithImage> GetSocialMenu(IPublishedContent publishedContentAtRoot)
        {
            var socialMenus = new List<SimpleLinkWithImage>();
            var socialMenuItems = publishedContentAtRoot.Value<IEnumerable<IPublishedElement>>(Constants.Fields.SocialLinks);

            foreach (var socialMenuItem in socialMenuItems)
            {
                var link = socialMenuItem.Value<IEnumerable<Umbraco.Web.Models.Link>>(Constants.Fields.Links)?.FirstOrDefault();

                var socialMenu = new SimpleLinkWithImage
                {
                    PublishedElement = socialMenuItem,
                    Title = socialMenuItem.GetStringData(Constants.Fields.Title),
                    Url = link?.Url,
                    Target = link?.Target,
                };

                var image = socialMenuItem.GetSingleMediaPicker(Constants.Fields.Image);
                socialMenu.Image = new Image
                {
                    PublishedContent = image,
                    Url = image?.Url(),
                    Alt = image?.Name
                };

                socialMenus.Add(socialMenu);
            }

            return socialMenus;
        }

        public static IEnumerable<SimpleLink> GetCategoryMenu(IPublishedContent publishedContentAtRoot)
        {
            return GetSimpleMenuFromAtRoot(publishedContentAtRoot, Constants.Fields.CategoryLinks);
        }

        public static IEnumerable<SimpleLink> GetPageMenu(IPublishedContent publishedContentAtRoot)
        {
            return GetSimpleMenuFromAtRoot(publishedContentAtRoot, Constants.Fields.PageLinks);
        }

        private static IEnumerable<SimpleLink> GetSimpleMenuFromAtRoot(IPublishedContent publishedContentAtRoot, string alias)
        {
            var menus = new List<SimpleLink>();
            var menuItems = publishedContentAtRoot.Value<IEnumerable<IPublishedElement>>(alias);

            foreach (var menuItem in menuItems)
            {
                var link = menuItem.Value<IEnumerable<Umbraco.Web.Models.Link>>(Constants.Fields.Links)?.FirstOrDefault();

                var footerMenu = new SimpleLink
                {
                    PublishedElement = menuItem,
                    Title = menuItem.GetStringData(Constants.Fields.Title),
                    Url = link?.Url,
                    Target = link?.Target
                };

                menus.Add(footerMenu);
            }

            return menus;
        }

        private static List<Link> ComputeChildLinks(IEnumerable<IPublishedElement> items)
        {
            var result = new List<Link>();
            if (items != null && items.Any())
            {
                foreach (var item in items)
                {
                    var link = item.Value<IEnumerable<Umbraco.Web.Models.Link>>(Constants.Fields.Links)?.FirstOrDefault();
                    var childItems = item.Value<IEnumerable<IPublishedElement>>(Constants.Fields.ChildLinks);

                    var headerMenu = new Link
                    {
                        Title = item.GetProperty(Constants.Fields.Title)?.GetValue()?.ToString(),
                        Url = link?.Url,
                        Target = link?.Target,

                        ChildLinks = ComputeChildLinks(childItems)
                    };

                    result.Add(headerMenu);
                }
            }

            return result;
        }
    }
}