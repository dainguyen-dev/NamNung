using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace NamNung.Web.Extensions
{
    public static class ContentExtention
    {
        public static string GetStringData(this IPublishedContent content, string alias)
        {
            return ValidProperty(content, alias) ? content.GetProperty(alias)?.GetValue()?.ToString() : string.Empty;
        }

        public static string GetStringData(this IPublishedContent content, string alias, string defaultValue)
        {
            var value = content.GetStringData(alias);
            return value.HasText() ? value : defaultValue;
        }

        public static string GetStringData(this IPublishedElement element, string alias)
        {
            return ValidProperty(element, alias) ? element.GetProperty(alias)?.GetValue()?.ToString() : string.Empty;
        }

        public static DateTime GetDateTime(this IPublishedContent content, string alias)
        {
            return ValidProperty(content, alias) ? content.Value<DateTime>(alias) : DateTime.MinValue;
        }

        public static IEnumerable<IPublishedContent> GetMediaPicker(this IPublishedContent content, string alias)
        {
            return ValidProperty(content, alias) ? content.Value<IEnumerable<IPublishedContent>>(alias): Enumerable.Empty<IPublishedContent>();
        }

        public static IEnumerable<IPublishedContent> GetMediaPicker(this IPublishedElement content, string alias)
        {
            if(!ValidProperty(content, alias))
            {
                return Enumerable.Empty<IPublishedContent>();
            }

            var result = content.Value<IEnumerable<IPublishedContent>>(alias);
            if(result is null)
            {
                return Enumerable.Empty<IPublishedContent>();
            }

            return result.Where(item => item != null);
        }

        public static IPublishedContent GetSingleMediaPicker(this IPublishedElement content, string alias)
        {
            return ValidProperty(content, alias) ? content.Value<IPublishedContent>(alias) : null;
        }

        private static bool ValidProperty(this IPublishedElement content, string alias)
        {
            var property = content.GetProperty(alias);
            return property != null && property.HasValue();
        }

        private static bool ValidProperty(this IPublishedContent content, string alias)
        {
            var property = content.GetProperty(alias);
            return property != null && property.HasValue();
        }
    }
}