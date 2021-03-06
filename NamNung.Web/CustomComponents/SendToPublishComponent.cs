using System.Threading;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;

namespace NamNung.Web.CustomComponents
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class SubscribeToPublishEventComposer : ComponentComposer<SubscribeToPublishEventComponent>
    { 
    }

    public class SubscribeToPublishEventComponent : IComponent
    {
        public void Initialize()
        {
            ContentService.SentToPublish += ContentService_SentToPublish;
            ContentService.Publishing += ContentService_Publishing;
        }

        private void ContentService_SentToPublish(IContentService sender, SendToPublishEventArgs<IContent> e)
        {
            return;
        }


        private void ContentService_Publishing(IContentService sender, ContentPublishingEventArgs e)
        {
            return;
        }

        public void Terminate()
        {
        }
    }
}