using System;
using System.Linq;
using sBlog.Net.Extensibility.Abstract;
using sBlog.Net.Extensibility.Events;
using sBlog.Net.Extensibility.Handlers;

namespace sBlog.Net.Extensibility
{
    public class EventHost : IPostEvent, IPageEvent
    {
        public event PostEventHandler.PostHandler PostDisplayed;
        public event EventHandler<PageEventArgs> PageDisplayed;

        public void RaisePostEvents(PostEventArgs eventArgs)
        {
            foreach (var evt in PostDisplayed.GetInvocationList()
                                             .Cast<PostEventHandler.PostHandler>()
                                             .Where(evt => evt != null))
            {
                evt(eventArgs);
            }
        }
    }
}
