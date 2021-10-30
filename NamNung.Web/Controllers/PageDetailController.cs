using NamNung.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.Membership;
using Umbraco.Core.Services;
using Umbraco.Web.Editors;
using Umbraco.Web.Security;

namespace NamNung.Web.Controllers
{
    [Umbraco.Web.Mvc.PluginController(nameof(NamNung))]
    public class PageDetailController : UmbracoAuthorizedJsonController
    {
        public CustomContentDashBoard GetAll()
        {

            var wrapper = new HttpContextWrapper(HttpContext.Current);
            var currentUserObj = wrapper.GetCurrentIdentity(true);
            var user = Services.UserService.GetByUsername(currentUserObj.Username);
            var userNotificationIds = Services.NotificationService.GetUserNotifications(user)
                .Select(notification => notification.EntityId.ToString()).Distinct();

            var contents = GetAllContent(userNotificationIds);
            var customContentDashBoard = GetCustomContentDashBoard(contents, user);

            return customContentDashBoard;
        }

        private IEnumerable<IContent> GetAllContent(IEnumerable<string> pathIds)
        {
            IContentService contentService = Services.ContentService;
            const string rootId = "0789bf43-f210-4ac2-bd73-ae0fcad1ff7f";

            var rootContent = contentService.GetById(Guid.Parse(rootId));
            var contents = new List<IContent>() { rootContent };
            contents.AddRange(contentService.GetPagedDescendants(rootContent.Id, 0, 1000000000, out _));
            contents = contents.Where(x => x.Path.Split(',').ToList().Intersect(pathIds).Any()).ToList();
            contents = contents.OrderByDescending(content => content.UpdateDate).ToList();

            return contents;
        }

        private CustomContentDashBoard GetCustomContentDashBoard(IEnumerable<IContent> contents, IUser user)
        {
            const string isoCode = "vi-VN";
            var userPath = user.StartContentIds;
            var pageDetails = new List<PageDetailApi>();

            foreach (var content in contents)
            {
                var culture = new CultureInfo(isoCode);
                if (userPath.Any() && !userPath.Any(x => content.Path.Contains(x.ToString())))
                {
                    continue;
                }

                if (content.PublishDate.Equals(content.UpdateDate))
                {
                    continue;
                }

                var pageDetail = new PageDetailApi()
                {
                    Id = content.Id.ToString(),
                    Url = $"content/content/edit/{content.Id}?mculture={isoCode}",
                    Name = content.Name,
                    UpdateDate = $"{content.UpdateDate.ToString(culture.DateTimeFormat.LongDatePattern, culture)} {content.UpdateDate.ToString(culture.DateTimeFormat.LongTimePattern, culture)}",
                    Status = $"{Umbraco.GetDictionaryValue(content.PublishedState.ToString())}"
                };

                var categoryId = content.ParentId.Equals(-1) ? content.Id.ToString() : content.Path.Split(',')?[2];
                if (categoryId != null)
                {
                    var categoryItem = Umbraco.Content(categoryId);
                    pageDetail.Category = categoryItem?.Name;
                }

                pageDetails.Add(pageDetail);
            }

            var customContentDashBoard = new CustomContentDashBoard()
            {
                Contents = pageDetails,
                Categories = pageDetails.Select(pageDetail => pageDetail.Category).Distinct(),
                Status = pageDetails.Select(pageDetail => pageDetail.Status).Distinct(),
            };

            return customContentDashBoard;
        }
    }
}