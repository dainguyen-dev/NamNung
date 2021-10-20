using NamNung.Web.Extensions;
using NamNung.Web.Factories.Models;
using System;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace NamNung.Web.Factories
{
    public static class SiteSettingsFactory
    {
        public static SiteSettings GetSiteSettings(IPublishedContent publishedContentAtRoot)
        {
            var siteSettings = new SiteSettings
            {
                PublishedContent = publishedContentAtRoot,
                ContactTitle = publishedContentAtRoot.GetStringData(Constants.Fields.ContactTitle),
                ContactAddress = publishedContentAtRoot.GetStringData(Constants.Fields.ContactAddress),
                ContactPhoneNumber = publishedContentAtRoot.GetStringData(Constants.Fields.ContactPhoneNumber),
                ContactFaxNumber = publishedContentAtRoot.GetStringData(Constants.Fields.ContactFaxNumber),
                ContactEmail = publishedContentAtRoot.GetStringData(Constants.Fields.ContactEmail),
                Copyright = string.Format(publishedContentAtRoot.GetStringData(Constants.Fields.Copyright), DateTime.Now.Year)
            };

            var logo = publishedContentAtRoot.Value<IPublishedContent>(Constants.Fields.Logo);
            siteSettings.LogoUrl = logo is null ? string.Empty : logo.Url();
            siteSettings.LogoAlt = logo is null ? string.Empty : logo.Value(Constants.Fields.Alt)?.ToString();
           
            return siteSettings;
        }
    }
}