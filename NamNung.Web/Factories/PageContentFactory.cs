using NamNung.Web.Extensions;
using NamNung.Web.Factories.Models;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace NamNung.Web.Factories
{
    public static class PageContentFactory
    {
        public static IEnumerable<PageContentDetail> GetAllSubPage(IPublishedContent publishedContent)
        {
            var pageContentDetails = new List<PageContentDetail>();

            foreach (var item in publishedContent.Descendants().Where(item => item.IsVisible()))
            {
                var pageContentDetail = GetPageContentDetail(item);

                if (pageContentDetail is null)
                    continue;

                pageContentDetails.Add(pageContentDetail);
            }

            return pageContentDetails;
        }

        public static IEnumerable<PageContentDetail> GetAllSubPageDetail(IPublishedContent publishedContent)
        {
            var pageContentDetails = GetAllSubPage(publishedContent);
            var pageContentDetailsWithTemplateAlias = pageContentDetails.Where(pageContentDetail => pageContentDetail.PublishedContent.GetTemplateAlias().ToLower().Contains(Constants.Templates.Detail.ToLower()));

            pageContentDetailsWithTemplateAlias = pageContentDetailsWithTemplateAlias.OrderByDescending(pageDetail => pageDetail.CreatedDate);
            return pageContentDetailsWithTemplateAlias;
        }

        public static IEnumerable<PageContentDetail> GetAllSubPageDetailWithShowOnHomePage(IPublishedContent publishedContent)
        {
            var pageContentDetails = GetAllSubPageDetail(publishedContent);
            var pageContentDetailsHighlightWithTemplate = pageContentDetails.Where(page => page.ShowOnHomePage);
            return pageContentDetailsHighlightWithTemplate;
        }

        public static IEnumerable<PageContentDetail> GetAllSubPageDetailWithHighight(IPublishedContent publishedContent)
        {
            var pageContentDetails = GetAllSubPageDetail(publishedContent);
            var pageContentDetailsHighlightWithTemplate = pageContentDetails.Where(page => page.Highlight);
            return pageContentDetailsHighlightWithTemplate;
        }

        public static IEnumerable<PageContentDetail> GetAllRelatedPageDetailByCategory(IPublishedContent publishedContentFolder, PageContentDetail pageContentDetail)
        {
            var details = GetAllSubPageDetail(publishedContentFolder).ToList();
            details.RemoveAll(page => page.PublishedContent.Id.Equals(pageContentDetail.PublishedContent.Id));

            var relatedDetailByCategory = details.Where(detail => IsRelatedCategory(detail, pageContentDetail));
            return relatedDetailByCategory;
        }

        private static bool IsRelatedCategory(PageContentDetail detailFist, PageContentDetail detailSecond)
        {
            if (detailFist.Category is null)
            {
                return false;
            }

            var categoryFirstIds = detailFist.Category.Select(item => item.Id);
            if (!categoryFirstIds.Any())
            {
                return false;
            }

            if (detailSecond.Category is null)
            {
                return false;
            }

            var categorySecondIds = detailSecond.Category.Select(item => item.Id);
            if (!categorySecondIds.Any())
            {
                return false;
            }

            return categoryFirstIds.Intersect(categorySecondIds).Any();

        }

        public static PageContentDetail GetPageContentDetail(IPublishedContent publishedContent)
        {
            var pageContentDetail = new PageContentDetail
            {
                PublishedContent = publishedContent,
                HeadingTitle = publishedContent.GetStringData(Constants.Fields.HeadingTitle),
                Title = publishedContent.GetStringData(Constants.Fields.Title),
                Description = publishedContent.GetStringData(Constants.Fields.Description),
                Body = publishedContent.GetStringData(Constants.Fields.Body),
                Url = publishedContent?.Url(),
                CreatedDate = publishedContent.GetDateTime(Constants.Fields.CreatedDate),
                Images = publishedContent.GetMediaPicker(Constants.Fields.Images).Select(image => new Image
                {
                    PublishedContent = image,
                    Url = image?.Url(),
                    Alt = image.Name
                }),
                Highlight = publishedContent.Value<bool>(Constants.Fields.Highlight),
                ShowOnHomePage = publishedContent.Value<bool>(Constants.Fields.showOnHomePage),
                Tags = publishedContent.Value<IEnumerable<string>>(Constants.Fields.Tags),
                Category = publishedContent.Value<IEnumerable<IPublishedContent>>(Constants.Fields.Category),
                Partner = new PageContentPartnerDetail()
                {
                    Address = publishedContent.Value<string>(Constants.Fields.Address),
                    PhoneNumber = publishedContent.Value<string>(Constants.Fields.PhoneNumber)
                }
            };

            var imageItem = publishedContent.GetSingleMediaPicker(Constants.Fields.Image);
            pageContentDetail.Image = imageItem != null ? new Image
            {
                PublishedContent = imageItem,
                Url = imageItem?.Url(),
                Alt = imageItem.Name
            } : pageContentDetail.Images.FirstOrDefault();

            var audioItem = publishedContent.GetSingleMediaPicker(Constants.Fields.AudioBody);
            pageContentDetail.AudioBody = audioItem != null ? new Audio
            {
                PublishedContent = audioItem,
                Url = audioItem?.Url(),
                Type = audioItem.Value<string>(Constants.Fields.UmbracoExtension)
            } : null;

            return pageContentDetail;
        }
    }
}