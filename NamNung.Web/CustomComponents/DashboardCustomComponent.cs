using System;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Dashboards;
using Umbraco.Web;
using Umbraco.Web.Dashboards;

namespace NamNung.Web.CustomComponents
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class DashboardCustomComponent : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Dashboards().Remove<ContentDashboard>();
            composition.Dashboards().Remove<RedirectUrlDashboard>();
        }
    }
}